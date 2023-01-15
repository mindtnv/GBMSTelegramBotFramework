using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace GBMSTelegramBotFramework;

public class CommandNotFoundHandler : UpdateHandlerBase
{
    private readonly CommandNotFoundHandlerOptions _options;

    public CommandNotFoundHandler(IOptions<CommandNotFoundHandlerOptions> options)
    {
        _options = options.Value;
    }

    public override Task OnMessageAsync(UpdateContext context)
    {
        if (context.Items.ContainsKey(CommandHandlerBase.CommandName) &&
            context.Items.TryGetValue(CommandHandlerBase.IsCommandExecuted, out var isCommandExecuted)
            && isCommandExecuted is false)
            return context.BotContext.Client.SendTextMessageAsync(context.Update.Message!.Chat.Id,
                _options.Message ?? throw new ArgumentNullException(nameof(_options.Message)));

        return Task.CompletedTask;
    }
}