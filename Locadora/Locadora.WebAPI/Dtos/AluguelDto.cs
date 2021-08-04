using System;
using System.Collections.Generic;

namespace Locadora.WebAPI.Dtos
{
    public class AluguelDto
    {
        public int Id { get; set; }
        public bool Aberto { get; set; } = true;
        public DateTime DataPedido { get; set; }
        public DateTime DataDevolucao { get; set; }
        public AluguelItemDto AluguelItem { get; set; }
        public readonly List<AluguelItemDto> aluguelItems;
    }
}
