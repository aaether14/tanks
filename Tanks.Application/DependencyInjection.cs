using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tanks.Application.Validation;
using Tanks.Domain.Factories;
using Tanks.Domain.Simulation;
using Tanks.Domain.Simulation.PathFinding;
using Tanks.Domain.Simulation.TanksAIs;

namespace Tanks.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddDependencies()
            .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly)
            .AddMediatR()
            .AddMapster();
    }

    private static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        return services
            .AddSingleton<IMapFactory, LabyrinthMapFactory>()
            .AddSingleton<IPathFinder, AStarPathFinder>()
            .AddSingleton<ITankAIChooser, InitialTankAIChooser>()
            .AddSingleton<Simulator>();
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
    }

    private static IServiceCollection AddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        return services
            .AddScoped<IMapper, Mapper>(_ => new Mapper(config));
    }
}