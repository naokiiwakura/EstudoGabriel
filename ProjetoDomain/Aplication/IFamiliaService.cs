using ProjetoDomain.Dto;
using ProjetoDomain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoDomain.Aplication
{
    public interface IFamiliaService
    {
        Familia SortearFamilia();
        int CalcularPontuacao(Familia familia);
    }
}