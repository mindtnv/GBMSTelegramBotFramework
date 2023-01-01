using GBMSTelegramBotFramework.Abstractions;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests;

public class ReplyWithMessageHandler : UpdateHandlerBase
{
    public override async Task OnMessageAsync(UpdateContext context)
    {
        var message = context.Update.Message;
        await context.Bot.Client.SendTextMessageAsync(message!.Chat.Id, message.Text!);
    }
}