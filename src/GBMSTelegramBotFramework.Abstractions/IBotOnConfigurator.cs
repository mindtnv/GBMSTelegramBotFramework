namespace GBMSTelegramBotFramework.Abstractions;

public interface IBotOnConfigurator
{
    IBotRegistrationConfigurator Configurator { get; }
}