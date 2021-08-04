using System;
using System.Collections.Generic;
using System.Linq;

namespace Locadora.Dominio.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        private readonly List<Aluguel> _alugueis;
        public IReadOnlyCollection<Aluguel> Alugueis => _alugueis;
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        
        public Cliente(string nome, DateTime dataNascimento, string cpf, string email, bool ativo, string rua, int numero, string bairro, string cep, string cidade, string estado)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Email = email;
            Ativo = ativo;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            _alugueis = new List<Aluguel>();
        }

        public bool PossuiPendencias()
        {
            if (Alugueis.Any(x => x.Aberto))
                return true;

            return false;
        }
        public void Alugar(Aluguel aluguel)
        {
            if (PossuiPendencias())
                throw new Exception("Cliente possui pendencias.");

            _alugueis.Add(aluguel);
        }

        public void Devolver(Aluguel aluguel)
        {
            aluguel.Aberto = false;
        }
    }
}
