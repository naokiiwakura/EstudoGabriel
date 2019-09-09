using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoDomain.Model
{
    public class Pessoa
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public DateTime DataDeNascimento { get; set; }
    }
}
