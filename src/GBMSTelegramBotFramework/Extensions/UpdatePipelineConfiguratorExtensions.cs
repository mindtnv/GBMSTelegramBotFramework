using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Extensions;

public static class UpdatePipelineConfiguratorExtensions
{
    public static IUpdatePipelineConfigurator UseMiddleware<TMiddleware>(this IUpdatePipelineConfigurator configurator)
        where TMiddleware : IUpdateMiddleware
    {
        configurator.Services.AddTransient(typeof(TMiddleware));
        configurator.Configure(x => x.UseMiddleware<TMiddleware>());
        return configurator;
    }

    public static IUpdatePipelineConfigurator UseHandler<THandler>(this IUpdatePipelineConfigurator configurator)
        where THandler : IUpdateHandler
    {
        configurator.Services.AddTransient(typeof(THandler));
        configurator.Configure(x => x.UseHandler<THandler>());
        return configurator;
    }

    public static IUpdatePipelineConfigurator UseCommand<TCommand>(this IUpdatePipelineConfigurator configurator)
        where TCommand : CommandHandlerBase
    {
        configurator.UseHandler<TCommand>();
        return configurator;
    }

    public static IUpdatePipelineConfigurator UseCommandNotFoundHandler(this IUpdatePipelineConfigurator configurator,
        string message)
    {
        configurator.Services.Configure<CommandNotFoundHandlerOptions>(x => x.Message = message);
        configurator.UseHandler<CommandNotFoundHandler>();
        return configurator;
    }

    public static IUpdatePipelineConfigurator UseReadMiddleware(this IUpdatePipelineConfigurator configurator)
    {
        configurator.UseMiddleware<ReadMiddleware>();
        return configurator;
    }
}