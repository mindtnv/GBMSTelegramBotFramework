using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GBMSTelegramBotFramework.Extensions;

public static class ServiceCollectionExtensions
{
    private static IServiceCollection AddUpdatePipelineBuilder(this IServiceCollection services)
    {
        services.TryAddSingleton<IUpdateMiddlewareFactory, UpdateMiddlewareFactory>();
        services.TryAddSingleton<IUpdateHandlerFactory, UpdateHandlerFactory>();
        services.TryAddSingleton<IUpdateContextFactory, UpdateContextFactory>();
        services.TryAdd(ServiceDescriptor.Transient(typeof(UpdateHandlerMiddleware<>),
            typeof(UpdateHandlerMiddleware<>)));
        return services;
    }

    public static IServiceCollection AddTelegramBot(this IServiceCollection services)
    {
        services.AddUpdatePipelineBuilder();
        return services;
    }
}