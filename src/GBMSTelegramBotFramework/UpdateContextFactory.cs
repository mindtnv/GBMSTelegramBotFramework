using GBMSTelegramBotFramework.Abstractions;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework;

public class UpdateContextFactory : IUpdateContextFactory
{
    public Task<UpdateContext> CreateAsync(IBot bot, Update update) =>
        Task.FromResult(new UpdateContext
        {
            Bot = bot,
            Update = update,
        });
}