using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Extensions;

public static class UpdatePipelineConfiguratorExtensions
{
    public static IUpdatePipelineConfigurator UseMiddleware<TMiddleware>(this IUpdatePipelineConfigurator configurator)
        where TMiddleware : IUpdateMiddleware
    {
        configurator.Services.AddScoped(typeof(TMiddleware));
        configurator.Configure(x => x.UseMiddleware<TMiddleware>());
        return configurator;
    }

    public static IUpdatePipelineConfigurator UseMiddleware<TMiddleware>(this IUpdatePipelineConfigurator configurator,
        TMiddleware instance)
        where TMiddleware : IUpdateMiddleware
    {
        configurator.Configure(x => x.UseMiddleware(instance));
        return configurator;
    }

    public static IUpdatePipelineConfigurator UseHandler<THandler>(this IUpdatePipelineConfigurator configurator)
        where THandler : IUpdateHandler
    {
        configurator.Services.AddScoped(typeof(THandler));
        configurator.Configure(x => x.UseHandler<THandler>());
        return configurator;
    }

    public static IUpdatePipelineConfigurator UseHandler<THandler>(this IUpdatePipelineConfigurator configurator,
        THandler instance)
        where THandler : IUpdateHandler
    {
        configurator.Configure(x => x.UseHandler(instance));
        return configurator;
    }
}