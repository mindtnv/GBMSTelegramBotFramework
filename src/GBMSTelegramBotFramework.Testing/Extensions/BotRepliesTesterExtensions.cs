using Telegram.Bot.Requests;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Testing.Extensions;

public static class BotRepliesTesterExtensions
{
    public static IBotRepliesTester RepliesWithText(this IBotRepliesTester tester, ChatId chatId, string text)
    {
        tester.RepliesWith(x => x is SendMessageRequest request && request.Text == text && request.ChatId == chatId);
        return tester;
    }
}