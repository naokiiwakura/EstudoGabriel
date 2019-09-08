using ProjetoDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDomain.Repository
{
    public interface IFamiliaRepository
    {
        List<Familia> Query();
    }
}
