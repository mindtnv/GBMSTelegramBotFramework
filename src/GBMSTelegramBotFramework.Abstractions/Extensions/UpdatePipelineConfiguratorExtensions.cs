using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Abstractions.Extensions;

public static class UpdatePipelineConfiguratorExtensions
{
    public static IUpdatePipelineConfigurator UseHandler<THandler>(this IUpdatePipelineConfigurator configurator)
        where THandler : IUpdateHandler
    {
        configurator.Services.AddTransient(typeof(THandler));
        configurator.Configure(x => x.UseHandler<THandler>());
        return configurator;
    }
}