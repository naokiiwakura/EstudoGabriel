using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoDomain.Dto
{
    public class SorteioDto
    {
        public int FamiliaId { get; set; }
        public int QuantidadeDeCriterios { get; set; }
        public int PontuacaoTotal { get; set; }
        public DateTime DataSorteio { get; set; }
    }
}
