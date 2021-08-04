using Locadora.Comuns.Enums;
using System;
using System.Collections.Generic;

namespace Locadora.Comuns.Dtos
{
    public class AluguelDto
    {
        public int Id { get; set; }
        public bool Aberto { get; set; }
        public Status Status { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataDevolucao { get; set; }
        public readonly List<AluguelItemDto> AluguelItens;
    }
}
