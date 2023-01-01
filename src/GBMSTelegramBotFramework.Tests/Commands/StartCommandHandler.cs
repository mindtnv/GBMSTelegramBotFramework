using GBMSTelegramBotFramework.Abstractions;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests.Commands;

public class StartCommandHandler : CommandHandlerBase
{
    public static readonly string Message = "Hello World!";
    public override string Name => "start";

    public override Task ExecuteAsync(UpdateContext context, string[] args)
    {
        var message = context.Update.Message;
        return context.Bot.Client.SendTextMessageAsync(message.Chat.Id, Message);
    }
}