
using Locadora.Comuns.Enums;

namespace Locadora.Dominio.Entidades
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoMidia TipoMidia { get; set; }
        public string Categoria { get; set; }
        public float Preco { get; set; }

        public Item(string nome, string descricao, TipoMidia tipoMidia, string categoria, float preco)
        {
            Nome = nome;
            Descricao = descricao;
            TipoMidia = tipoMidia;
            Categoria = categoria;
            Preco = preco;
        }
    }
}
