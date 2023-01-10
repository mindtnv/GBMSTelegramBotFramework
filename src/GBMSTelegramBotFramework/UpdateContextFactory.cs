using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework;

public class UpdateContextFactory : IUpdateContextFactory
{
    private readonly ICrossRequestContextStore _crossRequestContextStore;

    public UpdateContextFactory(ICrossRequestContextStore crossRequestContextStore)
    {
        _crossRequestContextStore = crossRequestContextStore;
    }

    public Task<UpdateContext> CreateAsync(IBot bot, Update update) =>
        Task.FromResult(new UpdateContext
        {
            Bot = bot,
            Update = update,
            CrossRequestContext =
                _crossRequestContextStore.Get(update.GetFlowId() ?? throw new ArgumentNullException()),
        });
}