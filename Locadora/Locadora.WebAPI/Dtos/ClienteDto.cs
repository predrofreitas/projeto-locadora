using System;
using System.Collections.Generic;

namespace Locadora.WebAPI.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public readonly List<AluguelDto> Alugueis;
    }
}
