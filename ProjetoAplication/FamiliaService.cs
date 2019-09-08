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

        public Familia SortearFamilia()
        {
            //TODO - implementar a regra de negocio do sorteio

            throw new NotImplementedException();
        }
    }
}
