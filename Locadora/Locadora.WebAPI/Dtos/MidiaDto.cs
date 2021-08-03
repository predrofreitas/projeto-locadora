namespace Locadora.WebAPI.Dtos
{
    public class MidiaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string TipoMidia { get; set; }
        public string Categoria { get; set; }
        public float Preco { get; set; }
    }
}
