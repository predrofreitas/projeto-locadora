using System;
using System.Collections.Generic;

namespace Locadora.WebAPI.Dtos
{
    public class AluguelDto
    {
        public int Id { get; set; }
        public bool Aberto { get; set; } = true;
        public List<MidiaDto> Midias { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataDevolucao { get; set; }
    }
}
