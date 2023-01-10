using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework;

public class UpdateContextFactory : IUpdateContextFactory
{
    private readonly ICrossRequestContextStoreProvider _contextStoreProvider;

    public UpdateContextFactory(ICrossRequestContextStoreProvider contextStoreProvider)
    {
        _contextStoreProvider = contextStoreProvider;
    }

    public Task<UpdateContext> CreateAsync(IBot bot, Update update) =>
        Task.FromResult(new UpdateContext
        {
            Bot = bot,
            Update = update,
            CrossRequestContext = _contextStoreProvider
                                  .Get(bot.Options.Name ?? throw new InvalidCastException("Bot name is null"))
                                  .Get(update.GetFromId() ??
                                       throw new ArgumentNullException()),
        });
}