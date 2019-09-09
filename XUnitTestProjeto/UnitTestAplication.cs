using System;
using Xunit;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using ProjetoDomain.Model;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ProjetoCrossCutting;
using ProjetoDomain.Aplication;
using ProjetoDomain.Repository;
using ProjetoAplication;
using System.Threading.Tasks;
using ProjetoDomain.Dto;
using System.Linq;
using System.Text;

namespace XUnitTestProjeto
{
    public class UnitTestProjetoAplication
    {
        private readonly IFamiliaService _familiaService;
        private readonly Mock<IFamiliaRepository> _familiaRepositoryMock = new Mock<IFamiliaRepository>();

        public UnitTestProjetoAplication()
        {
            //Realiza manualmente a injeção de dependência
            _familiaService = new FamiliaService(_familiaRepositoryMock.Object);
        }

        [Theory]
        [InlineData(1,6)] //Pretendente abaixo de 30 anos e renda até 900
        [InlineData(2, 6)] //Pretendente acima de 45 anos e renda entre 901 e 1500
        [InlineData(3, 8)] //Pretendente acima de 45 anos e renda até 900
        [InlineData(4, 3)] //Pretendente acima de 45 anos e renda acima de 2000
        [InlineData(5, 2)] //Pretendente abaixo de 30 anos e renda de 2000
        [InlineData(6, 4)] //Pretendente abaixo de 30 anos e renda de 901 e dependente acima de 18
        [InlineData(7, 6)] //Pretendente abaixo de 30 anos e renda de 901 e um dependente abaixo de 18
        [InlineData(8, 7)] //Pretendente abaixo de 30 anos e renda de 901 e três dependentes abaixo de 18
        public void CalcularPontuacao(int idFamlia, int pontuacao)
        {
            //Arranjo
            var familia = RetornaFamiliasParaCalcularPontos().FirstOrDefault(p => p.Id == idFamlia);

            //Ação
            var pontos = _familiaService.CalcularPontuacao(familia);

            //Confirmação
            Assert.Equal(pontuacao, pontos);
        }

        [Fact]
        public void SortearFamilia()
        {
            //Arranjo
            _familiaRepositoryMock.Setup(m => m.Query()).Returns(RetornaFamiliasParaCalcularPontos());

            //Ação
            var familia = _familiaService.SortearFamilia();

            //Confirmação
            Assert.Equal(3, familia.Id);
        }



        #region MocksParaTeste

        public List<Familia> RetornaFamiliasParaCalcularPontos()
        {
            var familias = new List<Familia>
            {
                new Familia
                {
                    Id = 1,
                    Rendas = new List<Renda> { new Renda { Valor = 500 }, new Renda { Valor = 400 } },
                    Pessoas = new List<Pessoa> { new Pessoa { DataDeNascimento = new DateTime(2002, 01, 01), Tipo = "Pretendente" } },
                },

                new Familia
                {
                    Id = 2,
                    Rendas = new List<Renda> { new Renda { Valor = 900 }, new Renda { Valor = 10 } },
                    Pessoas = new List<Pessoa> { new Pessoa { DataDeNascimento = new DateTime(1968, 01, 01), Tipo = "Pretendente" } },
                },

                new Familia
                {
                    Id = 3,
                    Rendas = new List<Renda> { new Renda { Valor = 200 }, new Renda { Valor = 300 } },
                    Pessoas = new List<Pessoa> { new Pessoa { DataDeNascimento = new DateTime(1968, 01, 01), Tipo = "Pretendente" } },
                },

                new Familia
                {
                    Id = 4,
                    Rendas = new List<Renda> { new Renda { Valor = 2000 }, new Renda { Valor = 5000 } },
                    Pessoas = new List<Pessoa> { new Pessoa { DataDeNascimento = new DateTime(1968, 01, 01), Tipo = "Pretendente" } },
                },

                new Familia
                {
                    Id = 5,
                    Rendas = new List<Renda> { new Renda { Valor = 300 }, new Renda { Valor = 1700 } },
                    Pessoas = new List<Pessoa> { new Pessoa { DataDeNascimento = new DateTime(2002, 01, 01), Tipo = "Pretendente" } },
                },

                new Familia
                {
                    Id = 6,
                    Rendas = new List<Renda> { new Renda { Valor = 600 }, new Renda { Valor = 301 } },
                    Pessoas = new List<Pessoa> {
                        new Pessoa { DataDeNascimento = new DateTime(1990, 01, 01), Tipo = "Pretendente" },
                        new Pessoa { DataDeNascimento = new DateTime(2000, 01, 01), Tipo = "Dependente" }
                    },
                },
                new Familia
                {
                    Id = 7,
                    Rendas = new List<Renda> { new Renda { Valor = 600 }, new Renda { Valor = 301 } },
                    Pessoas = new List<Pessoa> {
                        new Pessoa { DataDeNascimento = new DateTime(1990, 01, 01), Tipo = "Pretendente" },
                        new Pessoa { DataDeNascimento = new DateTime(2015, 01, 01), Tipo = "Dependente" },
                        new Pessoa { DataDeNascimento = new DateTime(1991, 01, 01), Tipo = "Dependente" },
                        new Pessoa { DataDeNascimento = new DateTime(1992, 01, 01), Tipo = "Dependente" },
                    },
                },
                new Familia
                {
                    Id = 8,
                    Rendas = new List<Renda> { new Renda { Valor = 600 }, new Renda { Valor = 301 } },
                    Pessoas = new List<Pessoa> {
                        new Pessoa { DataDeNascimento = new DateTime(1990, 01, 01), Tipo = "Pretendente" },
                        new Pessoa { DataDeNascimento = new DateTime(2015, 01, 01), Tipo = "Dependente" },
                        new Pessoa { DataDeNascimento = new DateTime(2014, 01, 01), Tipo = "Dependente" },
                        new Pessoa { DataDeNascimento = new DateTime(2013, 01, 01), Tipo = "Dependente" },
                    },
                },
            };

            return familias;
        }

        #endregion
    }
}
