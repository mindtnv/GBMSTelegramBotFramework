using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Commands;
using GBMSTelegramBotFramework.Extensions;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests.Commands;

public class SumReadCommand : CommandBase<SumReadCommand>
{
    public override void ConfigureDescriptor(ICommandOptionsBuilder builder)
    {
        builder.WithAliases(new[] {"/sum"});
    }

    public override async Task ExecuteAsync(UpdateContext context, string[] args)
    {
        var a = int.Parse(await context.ReadMessageAsync());
        var b = int.Parse(await context.ReadMessageAsync());
        await context.BotContext.Client.SendTextMessageAsync(context.Update.Message!.Chat.Id, (a + b).ToString());
    }
}