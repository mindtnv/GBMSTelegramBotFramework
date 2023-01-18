namespace GBMSTelegramBotFramework.Abstractions;

public interface IBotOnConfigurator
{
    IUpdatePipelineConfigurator Configurator { get; }
}