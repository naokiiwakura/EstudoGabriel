using ProjetoDomain.Dto;
using ProjetoDomain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoDomain.Aplication
{
    public interface IFamiliaService
    {
        SorteioDto SortearFamilia();
        SorteioDto CalcularPontuacaoTotal(Familia familia);
        int CalcularPontuacaoPorRenda(Familia familia);
        int CalcularPontuacaoPorIdadePretendente(Familia familia);
        int CalcularPontuacaoPeloNumeroDeDependentes(Familia familia);
        bool AlterarStatusFamilia(int codFamilia,int codStatus);
    }
}