using FluentAssertions;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class CommandHandlerBaseTests
{
    private IBot CreateTestBotWithPipeline(Action<IUpdatePipelineConfigurator> configure)
    {
        var services = new ServiceCollection();
        services.AddTelegramBot(bot =>
        {
            bot.UseTestingClient();
            bot.ConfigureUpdatePipeline(configure);
        });
        var provider = services.BuildServiceProvider();
        var bot = provider.GetRequiredService<IBot>();
        return bot;
    }

    [Test]
    public async Task WithoutArgsTest()
    {
        var bot = CreateTestBotWithPipeline(p => p.UseHandler<StartCommandHandler>());
        var update = UpdateBuilder.WithTextMessage("/start").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText("Hello World!"));
    }
 
    [Test]
    public async Task WithArgsTest()
    {
        var bot = CreateTestBotWithPipeline(p => p.UseHandler<SumCommandHandler>());
        var update = UpdateBuilder.WithTextMessage("/sum 5 10").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText("15"));
    }

    private class SumCommandHandler : CommandHandlerBase
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

    private class StartCommandHandler : CommandHandlerBase
    {
        public override string Name => "start";

        public override Task ExecuteAsync(UpdateContext context, string[] args)
        {
            var message = context.Update.Message;
            return context.Bot.Client.SendTextMessageAsync(message.Chat.Id, "Hello World!");
        }
    }
}