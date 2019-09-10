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
using Microsoft.Extensions.Caching.Memory;

namespace ProjetoProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorteioController : ControllerBase
    {
        private readonly IFamiliaService _service;
        private IMemoryCache _memoryCache;

        public SorteioController(IFamiliaService service, IMemoryCache memoryCache)
        {
            _service = service;
            _memoryCache = memoryCache;
        }

        public SorteioDto Get()
        {
            var familia = new SorteioDto();

            if (!_memoryCache.TryGetValue("sorteio", out familia))
            {
                familia = _service.SortearFamilia();
                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
                cacheOptions.SetPriority(CacheItemPriority.High);
                cacheOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
                _memoryCache.Set("sorteio", familia, cacheOptions);
            }

            return familia;
        }
    }
}