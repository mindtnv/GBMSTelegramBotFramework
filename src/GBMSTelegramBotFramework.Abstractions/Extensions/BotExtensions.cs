using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Abstractions.Extensions;

public static class BotExtensions
{
    public static async Task HandleUpdateAsync(this IBot bot, Update update)
    {
        var contextFactory = bot.Services.GetRequiredService<IUpdateContextFactory>();
        var context = await contextFactory.CreateAsync(bot, update);
        await bot.UpdateHandler(context);
    }
}