using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Extensions;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests.Commands;

public class SumReadCommandHandler : CommandHandlerBase
{
    public override string Name => "sum";

    public override async Task ExecuteAsync(UpdateContext context, string[] args)
    {
        var a = int.Parse(await context.ReadMessageAsync());
        var b = int.Parse(await context.ReadMessageAsync());
        await context.BotContext.Client.SendTextMessageAsync(context.Update.Message!.Chat.Id, (a + b).ToString());
    }
}