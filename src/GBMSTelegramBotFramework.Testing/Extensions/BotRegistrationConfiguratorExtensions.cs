using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Testing.Extensions;

public static class BotRegistrationConfiguratorExtensions
{
    public static IBotRegistrationConfigurator UseTestClient(this IBotRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.UseTelegramBotClient(new TestTelegramBotClient());
        return registrationConfigurator;
    }
}