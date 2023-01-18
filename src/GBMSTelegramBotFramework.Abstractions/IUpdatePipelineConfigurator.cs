using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdatePipelineConfigurator
{
    IServiceCollection Services { get; }
    IBotOnConfigurator On { get; }
    IUpdatePipelineConfigurator Configure(Action<IUpdatePipelineBuilder> configure);
    void ConfigureBuilder(IUpdatePipelineBuilder builder);
}