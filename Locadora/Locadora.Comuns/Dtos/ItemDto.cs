using Locadora.Comuns.Enums;
using System.Collections.Generic;

namespace Locadora.Comuns.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoMidia TipoMidia { get; set; }
        public string Categoria { get; set; }
        public float Preco { get; set; }
        public List<AluguelItemDto> Aluguel { get; set; }
    }
}
