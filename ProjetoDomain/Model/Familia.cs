using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoDomain.Model
{
    public class Familia
    {
        public string id { get; set; }
        public List<Pessoa> pessoas { get; set; }
        public List<Renda> rendas { get; set; }
        public string status { get; set; }
    }
}
