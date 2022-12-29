using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Testing.Extensions;

public static class BotRegistrationConfiguratorExtensions
{
    public static IBotRegistrationConfigurator UseTestingClient(
        this IBotRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.UseTelegramBotClient(new TelegramTestingBotClient());
        return registrationConfigurator;
    }
}