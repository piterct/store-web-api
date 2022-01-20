using Gestao.Business.Interfaces;
using Gestao.Data.Context;
using Gestao.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gestao.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<GestaoDbContext>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            return services;
        }
    }
}
