using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework;

internal class DefaultUpdateContext : UpdateContext
{
    private readonly IServiceScope _scope;

    public DefaultUpdateContext(IServiceScope scope, Update update, BotContext botContext,
        ICrossRequestContext crossRequestContext) : base(scope.ServiceProvider, update, botContext, crossRequestContext)
    {
        _scope = scope;
    }

    public override void Dispose()
    {
        _scope.Dispose();
    }
}