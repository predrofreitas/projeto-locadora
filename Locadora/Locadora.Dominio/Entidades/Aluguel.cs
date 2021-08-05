using Locadora.Comuns.Enums;
using System;
using System.Collections.Generic;

namespace Locadora.Dominio.Entidades
{
    public class Aluguel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public bool Aberto { get; set; } = true;
        public Status Status { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataDevolucao { get; set; }
        private readonly List<AluguelItem> _aluguelItens;
        public IReadOnlyCollection<AluguelItem> AluguelItens => _aluguelItens;
        private Dictionary<TipoMidia, float> MultasPorMidia;

        public Aluguel()
        {
            _aluguelItens = new List<AluguelItem>();
        }

        public void AdicionarItem(AluguelItem aluguelItem)
        {
            _aluguelItens.Add(aluguelItem);
        }

        public string CalcularMulta()
        {
            var diasAtraso = Convert.ToInt32((DateTime.Now - DataDevolucao).TotalDays);

            if (diasAtraso > 1)
            {
                var totalMulta = 0f;

                MultasPorMidia = new Dictionary<TipoMidia, float>()
                {
                    {TipoMidia.VHS, 0.2f },
                    {TipoMidia.CD, 0.3f },
                    {TipoMidia.DVD, 0.4f },
                    {TipoMidia.BLURAY, 0.5f },
                    {TipoMidia.VIDEOGAME, 1f },
                };

                _aluguelItens.ForEach(i =>
                {
                    totalMulta += i.Item.Preco * MultasPorMidia.GetValueOrDefault(i.Item.TipoMidia);
                });

                return $"Multa pendente no valor de: R$: {totalMulta}.";
            }

            return "Nenhuma multa pendente. Muito obrigado e volte sempre!";
        }
    }
}
