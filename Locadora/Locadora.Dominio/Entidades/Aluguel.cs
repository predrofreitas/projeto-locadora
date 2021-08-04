﻿using System;
using System.Collections.Generic;

namespace Locadora.Dominio.Entidades
{
    public class Aluguel
    {
        public int Id { get; set; }

        public Cliente Cliente { get; set; }
        public bool Aberto { get; set; } = true;
        public DateTime DataPedido { get; set; }
        public DateTime DataDevolucao { get; set; }

        public Aluguel()
        {
            List<AluguelItem> aluguelItems = new List<AluguelItem>();

        }
    }
}