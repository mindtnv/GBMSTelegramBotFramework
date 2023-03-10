using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Testing.Extensions;

public static class BotExtensions
{
    public static async Task HandleAndAssertAsync(this IBot bot, Update update,
        Action<ITelegramRequestsAsserter> assertAction)
    {
        await bot.HandleUpdateAsync(update);
        bot.Assert(assertAction);
    }

    public static void Assert(this IBot bot, Action<ITelegramRequestsAsserter> assertAction)
    {
        var client = bot.Client as TelegramTestingBotClient ??
                     throw new InvalidOperationException("Client is not TelegramTestingBotClient");
        var asserter = new TelegramRequestsAsserter(client.Requests);
        assertAction(asserter);
    }
}