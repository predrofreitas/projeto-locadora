using System;

namespace Locadora.Dominio.Entidades
{
    public class Estoque
    {
        public int Id { get; set; }
        public Midia Midia { get; set; }
        public int Quantidade { get; set; }


        public void InserirNoEstoque(int quantidade)
        {
            if (quantidade < 0)
            {
                throw new Exception("Quantidade da Midia no Estoque nao pode ser negativa.");
            }

            Quantidade = quantidade;
        }

        public void RetirarDoEstoque(int quantidade)
        {
            if(Quantidade - quantidade < 0)
            {
                throw new Exception("Midia sem estoque.");
            }

            Quantidade--;
        }
    }
}
