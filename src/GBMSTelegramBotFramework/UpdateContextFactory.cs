using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework;

public class UpdateContextFactory : IUpdateContextFactory
{
    private readonly IBotContextFactory _botContextFactory;
    private readonly IServiceProvider _serviceProvider;

    public UpdateContextFactory(IServiceProvider serviceProvider, IBotContextFactory botContextFactory)
    {
        _serviceProvider = serviceProvider;
        _botContextFactory = botContextFactory;
    }

    public Task<UpdateContext> CreateAsync(IBot bot, Update update)
    {
        var scope = _serviceProvider.CreateScope();
        var botContext = _botContextFactory.Create(bot);
        var featuresStore = bot.Features.Get<IUpdateContextFeaturesCollectionStore>() ??
                            throw new InvalidOperationException(
                                $"Register {nameof(IUpdateContextFeaturesCollectionStore)} to bot's features");
        var contextFeatures = featuresStore.GetFeaturesCollection(update.GetFromId() ??
                                                                  throw new InvalidOperationException(
                                                                      "Can't get user id from update"));
        return Task.FromResult(new DefaultUpdateContext(scope, update, botContext, contextFeatures) as UpdateContext);
    }
}