using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework;

public class UpdatePipelineConfigurator : IUpdatePipelineConfigurator
{
    private readonly List<Action<IUpdatePipelineBuilder>> _configurations = new();

    public UpdatePipelineConfigurator(IServiceCollection services, IFeaturesCollection botFeatures)
    {
        Services = services;
        BotFeatures = botFeatures;
        On = new DefaultUpdatePipelineOnConfigurator(this);
    }

    public IFeaturesCollection BotFeatures { get; }
    public IServiceCollection Services { get; }
    public IUpdatePipelineOnConfigurator On { get; }

    public IUpdatePipelineConfigurator Configure(Action<IUpdatePipelineBuilder> configure)
    {
        _configurations.Add(configure);
        return this;
    }

    public void ConfigureBuilder(IUpdatePipelineBuilder builder)
    {
        foreach (var configuration in _configurations)
            configuration(builder);
    }

    private class DefaultUpdatePipelineOnConfigurator : IUpdatePipelineOnConfigurator
    {
        public DefaultUpdatePipelineOnConfigurator(IUpdatePipelineConfigurator configurator)
        {
            Configurator = configurator;
        }

        public IUpdatePipelineConfigurator Configurator { get; }
    }
}