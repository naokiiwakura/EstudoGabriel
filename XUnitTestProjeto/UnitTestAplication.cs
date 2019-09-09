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

        private List<Familia> MockListaLivro()
        {
            var json = File.ReadAllText(@"../../../Mock/familias.json", Encoding.GetEncoding("iso-8859-1"));

            var familias = JsonConvert.DeserializeObject<List<Familia>>(json);

            return familias;
        }


        [Theory]
        [InlineData(1,6)] //Pretendente abaixo de 30 anos e renda até 900
        [InlineData(2, 6)] //Pretendente acima de 45 anos e renda entre 901 e 1500
        public void CalcularPontuacaoPorRenda(int idFamlia, int pontuacao)
        {
            //Arranjo
            var familia = RetornaFamiliasParaCalcularPontosPorRenda().Where(p => p.Id == idFamlia).FirstOrDefault();

            //Ação
            var pontos = _familiaService.CalcularPontuacao(familia);

            //Confirmação
            Assert.Equal(pontuacao, pontos);
        }



        #region MocksParaTeste

        public List<Familia> RetornaFamiliasParaCalcularPontosPorRenda()
        {
            var familias = new List<Familia>();

            familias.Add(new Familia {
                Id = 1,
                Rendas = new List<Renda> { new Renda { Valor = 500}, new Renda { Valor = 400 } },
                Pessoas = new List<Pessoa> { new Pessoa { DataDeNascimento = new DateTime(2002,01,01),Tipo ="Pretendente" } },
            });

            familias.Add(new Familia
            {
                Id = 2,
                Rendas = new List<Renda> { new Renda { Valor = 900 }, new Renda { Valor = 10 } },
                Pessoas = new List<Pessoa> { new Pessoa { DataDeNascimento = new DateTime(1968, 01, 01), Tipo = "Pretendente" } },
            });

            return familias;
        }

        #endregion

    }
}
