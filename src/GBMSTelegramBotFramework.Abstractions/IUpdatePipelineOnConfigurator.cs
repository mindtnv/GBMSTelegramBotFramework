namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdatePipelineOnConfigurator
{
    IUpdatePipelineConfigurator Configurator { get; }
}