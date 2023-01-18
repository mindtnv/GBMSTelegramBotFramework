using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework;

public class UpdatePipelineConfigurator : IUpdatePipelineConfigurator
{
    private readonly List<Action<IUpdatePipelineBuilder>> _configurations = new();

    public UpdatePipelineConfigurator(IServiceCollection services)
    {
        Services = services;
        On = new DefaultBotOnConfigurator(this);
    }

    public IServiceCollection Services { get; }
    public IBotOnConfigurator On { get; }

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

    private class DefaultBotOnConfigurator : IBotOnConfigurator
    {
        public DefaultBotOnConfigurator(IUpdatePipelineConfigurator configurator)
        {
            Configurator = configurator;
        }

        public IUpdatePipelineConfigurator Configurator { get; }
    }
}