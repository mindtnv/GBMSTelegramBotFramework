using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Commands;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests.Commands;

public class SumCommand : CommandBase<SumCommand>
{
    public override void ConfigureDescriptor(ICommandOptionsBuilder builder)
    {
        builder.WithAliases(new[] {"/sum"});
    }

    public override Task ExecuteAsync(UpdateContext context, string[] args)
    {
        var sum = args switch
        {
            [var a, var b] => int.Parse(a) + int.Parse(b),
            [var a]        => int.Parse(a),
            _              => 0,
        };

        return context.BotContext.Client.SendTextMessageAsync(context.Update.Message.Chat.Id, sum.ToString());
    }
}