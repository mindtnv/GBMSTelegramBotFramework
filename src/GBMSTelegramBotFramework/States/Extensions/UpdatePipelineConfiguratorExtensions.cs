using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Extensions;

namespace GBMSTelegramBotFramework.States.Extensions;

public static class UpdatePipelineConfiguratorExtensions
{
    public static IUpdatePipelineConfigurator UseStates(this IUpdatePipelineConfigurator configurator)
    {
        configurator.UseMiddleware<StateMiddleware>();
        return configurator;
    }
}