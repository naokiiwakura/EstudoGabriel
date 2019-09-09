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
        public SorteioDto SortearFamilia()
        {
            var familiasDisponiveis = _repo.Query().Where(p => p.Status == 0).ToList();
            Familia familiaSelecionada = null;
            int pontuacaoDaFamiliaSelecionada = 0;

            foreach (var familia in familiasDisponiveis)
            {
                var pontos = CalcularPontuacaoTotal(familia);

                if(pontuacaoDaFamiliaSelecionada < pontos.PontuacaoTotal)
                {
                    familiaSelecionada = familia;
                    pontuacaoDaFamiliaSelecionada = pontos.PontuacaoTotal;
                }
            }

            var dtoRetorno = new SorteioDto
            {
                FamiliaId = familiaSelecionada.Id,
                PontuacaoTotal = pontuacaoDaFamiliaSelecionada,
                DataSorteio = DateTime.Now
            };

            return dtoRetorno;
        }


        /// <summary>
        /// Realiza o cálculo em pontos da família
        /// </summary>
        /// <param name="familia"></param>
        /// <returns></returns>
        public SorteioDto CalcularPontuacaoTotal(Familia familia)
        {
            var pontosRenda = CalcularPontuacaoPorRenda(familia);

            var pontosIdadePretendente = CalcularPontuacaoPorIdadePretendente(familia);

            var pontosNumeroDependentes = CalcularPontuacaoPeloNumeroDeDependentes(familia);

            var criteriosAtendidos = 0;

            criteriosAtendidos += pontosRenda > 0 ? 1 : 0;

            criteriosAtendidos += pontosIdadePretendente > 0 ? 1 : 0;

            criteriosAtendidos += pontosNumeroDependentes > 0 ? 1 : 0;

            var pontosTotais = pontosRenda + pontosIdadePretendente + pontosNumeroDependentes;

            var sorteioFamilia = new SorteioDto
            {
                FamiliaId = familia.Id,
                QuantidadeDeCriterios = criteriosAtendidos,
                DataSorteio = DateTime.Now,
                PontuacaoTotal = pontosTotais
            };

            return sorteioFamilia;
        }

        public int CalcularPontuacaoPorRenda(Familia familia)
        {
            var rendaFamiliar = familia.Rendas.Sum(p => p.Valor);
            var pontuacao = 0;
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

            return pontuacao;
        }

        public int CalcularPontuacaoPorIdadePretendente(Familia familia)
        {

            //pontuação pela idade do pretendente
            var pretendente = familia.Pessoas.FirstOrDefault(p => p.Tipo == "Pretendente");
            var pontuacao = 0;
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
            return pontuacao;
        }

        public int CalcularPontuacaoPeloNumeroDeDependentes(Familia familia)
        {
            //Pontuação pela quantidade de dependentes
            var quantidadeDependentes = familia.Pessoas.Where(p => p.Tipo == "Dependente" && CalcularIdade.Age(p.DataDeNascimento) < 18).Count();
            var pontuacao = 0;
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
