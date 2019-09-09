using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoDomain.Model
{
    public class Familia
    {
        public string Id { get; set; }
        public List<Pessoa> Pessoas { get; set; }
        public List<Renda> Rendas { get; set; }
        public int Status { get; set; }
    }
}