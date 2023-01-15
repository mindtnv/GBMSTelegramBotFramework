using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework;

public class UpdateContextFactory : IUpdateContextFactory
{
    private readonly IBotContextFactory _botContextFactory;
    private readonly ICrossRequestContextStoreProvider _contextStoreProvider;
    private readonly IServiceProvider _serviceProvider;

    public UpdateContextFactory(ICrossRequestContextStoreProvider contextStoreProvider,
        IServiceProvider serviceProvider, IBotContextFactory botContextFactory)
    {
        _contextStoreProvider = contextStoreProvider;
        _serviceProvider = serviceProvider;
        _botContextFactory = botContextFactory;
    }

    public Task<UpdateContext> CreateAsync(IBot bot, Update update)
    {
        var scope = _serviceProvider.CreateScope();
        var botContext = _botContextFactory.Create(bot);
        var crossRequestContext = _contextStoreProvider
                                  .Get(bot.Options.Name ?? throw new InvalidCastException("Bot name is null"))
                                  .Get(update.GetFromId() ??
                                       throw new ArgumentNullException());

        return Task.FromResult(
            new DefaultUpdateContext(scope, update, botContext, crossRequestContext) as UpdateContext);
    }
}