using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework;

internal class DefaultUpdateContext : UpdateContext
{
    private readonly IServiceScope _scope;
    public override IUpdateContextReply Reply { get; }

    public DefaultUpdateContext(IServiceScope scope, Update update, BotContext botContext,
        IFeaturesCollection features) : base(scope.ServiceProvider, update, botContext, features)
    {
        _scope = scope;
        Reply = new UpdateContextReply(this);
    }

    public override void Dispose()
    {
        _scope.Dispose();
    }
}