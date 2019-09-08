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
            var json = File.ReadAllText(@"../../../Mock/books.json", Encoding.GetEncoding("iso-8859-1"));

            var familias = JsonConvert.DeserializeObject<List<Familia>>(json);

            return familias;
        }



    }
}
