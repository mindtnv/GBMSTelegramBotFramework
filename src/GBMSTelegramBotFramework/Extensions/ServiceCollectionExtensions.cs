using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GBMSTelegramBotFramework.Extensions;

public static class ServiceCollectionExtensions
{
    private static IServiceCollection AddUpdatePipelineBuilderServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IUpdateMiddlewareFactory, UpdateMiddlewareFactory>();
        services.TryAddSingleton<IUpdateHandlerFactory, UpdateHandlerFactory>();
        services.TryAddSingleton<IUpdateContextFactory, UpdateContextFactory>();
        services.TryAdd(ServiceDescriptor.Scoped(typeof(UpdateHandlerMiddleware<>),
            typeof(UpdateHandlerMiddleware<>)));
        return services;
    }

    public static IServiceCollection AddTelegramBot(this IServiceCollection services,
        Action<IBotRegistrationConfigurator> configure)
    {
        services.AddUpdatePipelineBuilderServices();
        var configurator = new BotRegistrationConfigurator(services);
        configure(configurator);
        configurator.Configure();
        return services;
    }

    public static IServiceCollection UseTelegramLongPulling(this IServiceCollection services)
    {
        services.AddHostedService<LongPullingHostedService>();
        return services;
    }
}