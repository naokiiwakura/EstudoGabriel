using ProjetoAplication.Tools;
using ProjetoDomain.Aplication;
using ProjetoDomain.Dto;
using ProjetoDomain.Model;
using ProjetoDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoAplication
{
    public class FamiliaService : IFamiliaService
    {
        private readonly IFamiliaRepository _repo;

        public FamiliaService(IFamiliaRepository repo)
        {
            _repo = repo;
        }


        /// <summary>
        /// Realiza o sorteio da família baseado em sua pontuação
        /// </summary>
        /// <returns></returns>
        public Familia SortearFamilia()
        {
            var familiasDisponiveis = _repo.Query().Where(p => p.Status == 0).ToList();
            Familia familiaSelecionada = null;
            int pontuacaoDaFamiliaSelecionada = 0;

            foreach (var familia in familiasDisponiveis)
            {
                var pontos = CalcularPontuacao(familia);

                if(pontuacaoDaFamiliaSelecionada < pontos)
                {
                    familiaSelecionada = familia;
                    pontuacaoDaFamiliaSelecionada = pontos;
                }
            }

            return familiaSelecionada;
        }


        /// <summary>
        /// Realiza o cálculo em pontos da família
        /// </summary>
        /// <param name="familia"></param>
        /// <returns></returns>
        public int CalcularPontuacao(Familia familia)
        {
            var rendaFamiliar = familia.Rendas.Sum(p => p.Valor);
            int pontuacao = 0;

            //Pontuação po renda
            if (rendaFamiliar <= 900)
            {
                pontuacao += 5;
            }
            else if (rendaFamiliar > 900 && rendaFamiliar <= 1500)
            {
                pontuacao += 3;
            }
            else if (rendaFamiliar > 1500 && rendaFamiliar <= 2000)
            {
                pontuacao += 1;
            }


            //pontuação pela idade do pretendente
            var pretendente = familia.Pessoas.FirstOrDefault(p => p.Tipo == "Pretendente");
            if (pretendente != null)
            {
                var idadePretendente = CalcularIdade.Age(pretendente.DataDeNascimento);
                if (idadePretendente >= 45)
                {
                    pontuacao += 3;
                }
                else if (idadePretendente < 45 && idadePretendente >= 30)
                {
                    pontuacao += 2;
                }
                else if (idadePretendente < 30)
                {
                    pontuacao += 1;
                }
            }


            //Pontuação pela quantidade de dependentes
            var quantidadeDependentes = familia.Pessoas.Where(p => p.Tipo == "Dependente" && CalcularIdade.Age(p.DataDeNascimento) < 18).Count();
            if (quantidadeDependentes >= 3)
            {
                pontuacao += 3;
            }
            else if (quantidadeDependentes > 0)
            {
                pontuacao += 2;
            }

            return pontuacao;
        }
    }
}
