using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdateContextFactory
{
    Task<UpdateContext> CreateAsync(IBot bot, Update update);
}