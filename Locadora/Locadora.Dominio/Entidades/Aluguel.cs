using System;
using System.Collections.Generic;

namespace Locadora.Dominio.Entidades
{
    public class Aluguel
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public bool Aberto { get; set; } = true;
        public readonly List<Midia> _midias;
        public IReadOnlyCollection<Midia> Midias => _midias;
        public DateTime DataPedido { get; set; }
        public DateTime DataDevolucao { get; set; }

        public Aluguel()
        {
            List<Midia> _midias = new List<Midia>();
        }
    }
}
