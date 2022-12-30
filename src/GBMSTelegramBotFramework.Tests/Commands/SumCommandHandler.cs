using GBMSTelegramBotFramework.Abstractions;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests.Commands;

public class SumCommandHandler : CommandHandlerBase
{
    public override string Name => "sum";

    public override Task ExecuteAsync(UpdateContext context, string[] args)
    {
        var sum = args switch
        {
            [var a, var b, ] => Int32.Parse(a) + Int32.Parse(b),
            [var a]          => Int32.Parse(a),
            _                => 0,
        };

        return context.Bot.Client.SendTextMessageAsync(context.Update.Message.Chat.Id, sum.ToString());
    }
}