using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Commands;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests.Commands.Examples;

public class IncrementCommand : CommandBase<IncrementCommand>
{
    public override void ConfigureDescriptor(ICommandOptionsBuilder builder)
    {
        builder.WithAliases(new[] {"/increment"});
    }

    public override Task ExecuteAsync(UpdateContext context, string[] args)
    {
        var counter = context.Features.Get<TestingCounter>();
        if (counter == null)
        {
            counter = new TestingCounter();
            context.Features.Set(counter);
        }

        counter.Increment();
        return context.BotContext.Client.SendTextMessageAsync(context.Update.Message!.Chat.Id,
            counter.Value.ToString());
    }
}