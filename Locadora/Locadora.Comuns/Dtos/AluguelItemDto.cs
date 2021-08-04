namespace Locadora.Comuns.Dtos
{
    public class AluguelItemDto
    {
        public int Id { get; set; }
        public AluguelDto Aluguel { get; set; }
        public ItemDto Item { get; set; }
    }
}
