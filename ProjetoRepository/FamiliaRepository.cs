using ProjetoDomain.Model;
using ProjetoDomain.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRepository
{
    public class FamiliaRepository : IFamiliaRepository
    {
        public List<Familia> Query()
        {
            var json = File.ReadAllText(@"../ProjetoRepository/Mock/familias.json", Encoding.GetEncoding("iso-8859-1"));

            var familias = JsonConvert.DeserializeObject<List<Familia>>(json);

            return familias;
        }
    }
}