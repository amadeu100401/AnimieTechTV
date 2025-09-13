using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace AnimieTechTv.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjectionExtension).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
