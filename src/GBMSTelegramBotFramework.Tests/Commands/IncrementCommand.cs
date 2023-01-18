using GBMSTelegramBotFramework.Abstractions;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests.Commands;

public class IncrementCommand : CommandHandlerBase
{
    public override string Name => "increment";

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