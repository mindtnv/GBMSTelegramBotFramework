using Telegram.Bot.Requests.Abstractions;

namespace GBMSTelegramBotFramework.Testing;

public interface IBotRepliesTester
{
    BotRepliesTester RepliesWith(Func<IRequest, bool> matcher);
    public bool IsMatched();
}