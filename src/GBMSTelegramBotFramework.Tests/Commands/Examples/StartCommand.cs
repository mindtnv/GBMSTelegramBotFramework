using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Commands;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests.Commands.Examples;

public class StartCommand : CommandBase<StartCommand>
{
    public static readonly string Message = "Hello World!";

    public override void ConfigureDescriptor(ICommandOptionsBuilder builder)
    {
        builder.WithAliases(new[] {"/start", "start"});
    }

    public override Task ExecuteAsync(UpdateContext context, string[] args)
    {
        var message = context.Update.Message;
        return context.BotContext.Client.SendTextMessageAsync(message.Chat.Id, Message);
    }
}