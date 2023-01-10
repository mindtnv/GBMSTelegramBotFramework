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
        services.TryAddSingleton<ICrossRequestContextStoreProvider, CrossRequestContextStoreProvider>();
        services.TryAddSingleton<ICrossRequestContextStore, CrossRequestContextStore>();
        services.TryAddTransient<StopMiddleware>();
        services.TryAddSingleton<IBotProvider, BotProvider>();
        services.TryAdd(ServiceDescriptor.Transient(typeof(UpdateHandlerMiddleware<>),
            typeof(UpdateHandlerMiddleware<>)));
        return services;
    }

    public static IServiceCollection AddTelegramBot(this IServiceCollection services,
        Action<IBotRegistrationConfigurator> configure)
    {
        services.AddUpdatePipelineBuilderServices();
        var configurator = new BotRegistrationConfigurator(services);
        configure(configurator);
        configurator.Register();
        return services;
    }

    public static IServiceCollection UseTelegramLongPulling(this IServiceCollection services)
    {
        if (services.Any(x => x.ImplementationType == typeof(WebhookHostedService)))
            throw new InvalidOperationException("You can't use long pulling and webhook at the same time");

        services.AddHostedService<LongPullingHostedService>();
        return services;
    }

    public static IServiceCollection UseTelegramWebHook(this IServiceCollection services) =>
        UseTelegramWebHook(services, _ => { });

    public static IServiceCollection UseTelegramWebHook(this IServiceCollection services,
        Action<WebhookOptions> configure)
    {
        if (services.Any(x => x.ImplementationType == typeof(LongPullingHostedService)))
            throw new InvalidOperationException("You can't use long pulling and webhook at the same time");

        services.AddSingleton<IWebhookUrlResolver, WebhookUrlResolver>();
        services.AddHostedService<WebhookHostedService>();
        services.Configure(configure);
        return services;
    }
}