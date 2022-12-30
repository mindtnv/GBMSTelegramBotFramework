using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;
using GBMSTelegramBotFramework.Tests.Commands;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class CommandHandlerBaseTests
{
    private class StartCommandHandler : CommandHandlerBase
    {
        public override string Name => "start";

        public override Task ExecuteAsync(UpdateContext context, string[] args)
        {
            var message = context.Update.Message;
            return context.Bot.Client.SendTextMessageAsync(message.Chat.Id, "Hello World!");
        }
    }

    [Test]
    public async Task WithArgsTest()
    {
        var bot = Utils.CreateTestBotWithPipeline(p => p.UseHandler<SumCommandHandler>());
        var update = UpdateBuilder.WithTextMessage("/sum 5 10").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText("15"));
    }

    [Test]
    public async Task WithoutArgsTest()
    {
        var bot = Utils.CreateTestBotWithPipeline(p => p.UseHandler<StartCommandHandler>());
        var update = UpdateBuilder.WithTextMessage("/start").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText("Hello World!"));
    }
}