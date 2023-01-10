using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Extensions;

public static class UpdatePipelineConfiguratorExtensions
{
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