using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class BotContextFactory : IBotContextFactory
{
    public BotContext Create(IBot bot) =>
        new()
        {
            Options = bot.Options,
            Client = bot.Client,
            Features = bot.Features,
        };
}