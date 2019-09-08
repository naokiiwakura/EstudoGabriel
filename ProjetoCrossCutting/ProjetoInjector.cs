using ProjetoAplication;
using ProjetoDomain.Aplication;
using ProjetoDomain.Repository;
using ProjetoRepository;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ProjetoCrossCutting
{
    public class ProjetoInjector
    {
        public static void RegistrarServicos(IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IFamiliaRepository, FamiliaRepository>();

            //Services
            services.AddScoped<IFamiliaService, FamiliaService>();
        }
    }
}
