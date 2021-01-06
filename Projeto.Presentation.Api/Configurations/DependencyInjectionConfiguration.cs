using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projeto.Infra.Data.Contracts;
using Projeto.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BDProjetoTDD");

            services.AddTransient<IProdutoRepository>
                (map => new ProdutoRepository(connectionString));
        }
    }
}
