using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace GBMSTelegramBotFramework.Extensions;

public static class ServiceCollectionExtensions
{
    private static IServiceCollection AddUpdatePipelineBuilderServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IUpdateMiddlewareFactory, UpdateMiddlewareFactory>();
        services.TryAddSingleton<IUpdateHandlerFactory, UpdateHandlerFactory>();
        services.TryAddSingleton<IUpdateContextFactory, UpdateContextFactory>();
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
        configurator.Configure();
        return services;
    }

    public static IServiceCollection UseTelegramLongPulling(this IServiceCollection services)
    {
        if (services.Contains(new ServiceDescriptor(typeof(IHostedService), typeof(WebhookUrlResolver))))
            throw new InvalidOperationException("You can't use long pulling and webhook at the same time");

        services.AddHostedService<LongPullingHostedService>();
        return services;
    }

    public static IServiceCollection UseTelegramWebHook(this IServiceCollection services)
    {
        if (services.Contains(new ServiceDescriptor(typeof(IHostedService), typeof(LongPullingHostedService))))
            throw new InvalidOperationException("You can't use long pulling and webhook at the same time");

        services.AddSingleton<IWebhookUrlResolver, WebhookUrlResolver>();
        services.AddHostedService<WebhookHostedService>();
        return services;
    }
}