using System;
using System.Collections.Generic;

namespace Locadora.Dominio.Entidades
{
    public class Aluguel
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public bool Aberto { get; set; } = true;
        public DateTime DataPedido { get; set; }
        public DateTime DataDevolucao { get; set; }
        private readonly List<AluguelItem> _aluguelItens;
        public IReadOnlyCollection<AluguelItem> AluguelItens => _aluguelItens;

        public Aluguel()
        {
            _aluguelItens = new List<AluguelItem>();
        }

        public void AdicionarItem(AluguelItem aluguelItem)
        {
            _aluguelItens.Add(aluguelItem);
        }
    }
}
