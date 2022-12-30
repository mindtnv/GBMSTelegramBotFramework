using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Extensions;

public static class BotRegistrationConfiguratorExtensions
{
    public static IBotRegistrationConfigurator UseCommandNotFoundHandler(this IBotRegistrationConfigurator configurator,
        string message)
    {
        configurator.ConfigureUpdatePipeline(p => p.UseCommandNotFoundHandler(message));
        return configurator;
    }
}