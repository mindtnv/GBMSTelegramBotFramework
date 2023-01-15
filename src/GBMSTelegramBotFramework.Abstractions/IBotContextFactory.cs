namespace GBMSTelegramBotFramework.Abstractions;

public interface IBotContextFactory
{
    BotContext Create(IBot bot);
}