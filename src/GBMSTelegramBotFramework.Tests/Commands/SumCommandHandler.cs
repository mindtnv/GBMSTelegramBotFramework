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
            [var a, var b, ] => int.Parse(a) + int.Parse(b),
            [var a]          => int.Parse(a),
            _                => 0,
        };

        return context.Bot.Client.SendTextMessageAsync(context.Update.Message.Chat.Id, sum.ToString());
    }
}