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
        int CalcularPontuacaoTotal(Familia familia);
        int CalcularPontuacaoPorRenda(Familia familia);
        int CalcularPontuacaoPorIdadePretendente(Familia familia);
        int CalcularPontuacaoPeloNumeroDeDependentes(Familia familia);
    }
}