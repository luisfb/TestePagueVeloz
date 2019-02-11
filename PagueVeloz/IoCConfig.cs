using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PagueVeloz.App.Cadastros;
using PagueVeloz.Domain.Interfaces;
using PagueVeloz.Infra;
using PagueVeloz.Infra.Repositories;

namespace PagueVeloz.API
{
    public static class IoCConfig
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<ICadastroService, CadastroService>();
            services.AddScoped<IRepository, GenericRepository>();
            services.AddScoped<IUnitOfWork, EfContext>();
            services.AddScoped<DbContext, EfContext>();
        }

    }
}
