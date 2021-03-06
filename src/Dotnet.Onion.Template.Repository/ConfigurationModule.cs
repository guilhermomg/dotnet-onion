using Dotnet.Onion.Template.Domain.Company.Repository;
using Dotnet.Onion.Template.Domain.Store.Repository;
using Dotnet.Onion.Template.Repository.Context;
using Dotnet.Onion.Template.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Onion.Template.Repository
{
    public static class ConfigurationModule
    {
        public static void RegisterRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextApp>(c =>
            {
                c.UseSqlServer(configuration.GetConnectionString("AuxConnection"));
            });

            // Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            services.AddScoped(typeof(UnitOfWork<>));

            services.Configure<DatabaseOptions>(configuration.GetSection("ConnectionStrings"));

            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IStoreRepository, StoreRepository>();

        }
    }
}
