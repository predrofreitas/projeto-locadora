using System.Collections.Generic;

namespace Locadora.Dominio.Entidades
{
    public class Midia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string TipoMidia { get; set; }
        public string Categoria { get; set; }
        public float Preco { get; set; }

        public readonly List<AluguelItem> _alugueisItem;
        public IReadOnlyCollection<AluguelItem> AluguelItem => _alugueisItem;


        public Midia()
        {
            //List<Aluguel> _alugueis = new List<Aluguel>();
        }
    }
}
