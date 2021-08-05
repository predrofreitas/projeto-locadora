using System;

namespace Locadora.Dominio.Entidades
{
    public class Estoque
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Quantidade { get; set; }

        public Estoque()
        {
        }

        public void RetirarDoEstoque()
        {
            if(Quantidade < 1)
            {
                throw new Exception("Midia sem estoque.");
            }

            Quantidade--;
        }

        public void ReporNoEstoque()
        {
            Quantidade++;
        }
    }
}
