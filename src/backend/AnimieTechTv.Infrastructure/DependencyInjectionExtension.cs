using AnimieTechTv.Domain.Repositories;
using AnimieTechTv.Domain.Repositories.Animie;
using AnimieTechTv.Infrastructure.DataAccess;
using AnimieTechTv.Infrastructure.DataAccess.Repositories;
using AnimieTechTv.Infrastructure.Extensions;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AnimieTechTv.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepository(services);
        AddFluentMigratior(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)   
    {
        var connectionString = configuration.ConnectionString();

        services.AddDbContext<AnimieTechTvDbContext>(dbContextOpt =>
        {
            dbContextOpt.UseSqlServer(connectionString);
        });
    }

    private static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IAnimieReadOnlyRepository, AnimieRepository>();
        services.AddScoped<IAnimieWriteOnlyRepository, AnimieRepository>();
        services.AddScoped<IAnimieDeleteRepository, AnimieRepository>();

        services.AddScoped<IUnityWork, UnityWork>();
    }

    private static void AddFluentMigratior(IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(opt =>
            {
                opt.AddSqlServer()
                    .WithGlobalConnectionString(configuration.ConnectionString())
                    .ScanIn(Assembly.Load("AnimieTechTv.Infrastructure"))
                    .For.All();
            });
    }
}
