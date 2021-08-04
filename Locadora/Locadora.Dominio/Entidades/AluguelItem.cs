namespace Locadora.Dominio.Entidades
{
    public class AluguelItem
    {
        public int Id { get; set; }
        public Aluguel Aluguel { get; set; }
        public Item Item { get; set; }

        public AluguelItem()
        {
        }
    }
}
