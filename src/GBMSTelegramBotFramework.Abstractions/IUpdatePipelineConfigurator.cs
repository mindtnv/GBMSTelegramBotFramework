using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdatePipelineConfigurator
{
    IFeaturesCollection BotFeatures { get; }
    IServiceCollection Services { get; }
    IUpdatePipelineOnConfigurator On { get; }
    IUpdatePipelineConfigurator Configure(Action<IUpdatePipelineBuilder> configure);
    void ConfigureBuilder(IUpdatePipelineBuilder builder);
}