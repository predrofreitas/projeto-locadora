using System;
using System.Collections.Generic;

namespace Locadora.Dominio.Entidades
{
    public class AluguelItem
    {
        public int Id { get; set; }

        public Aluguel Aluguel { get; set; }
        public Midia Midia { get; set; }


        public AluguelItem()
        {
            List<Midia> _midias = new List<Midia>();

        }
    }
}
