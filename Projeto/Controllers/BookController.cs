using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoAplication;
using ProjetoDomain.Aplication;
using ProjetoDomain.Dto;
using ProjetoDomain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorteioController : ControllerBase
    {
        private readonly IFamiliaService _service;

        public SorteioController(IFamiliaService service)
        {
            _service = service;
        }

        public Familia Get()
        {            
            return _service.SortearFamilia();
        }
    }
}