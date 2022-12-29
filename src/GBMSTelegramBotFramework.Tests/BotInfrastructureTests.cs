using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class BotInfrastructureTests
{
    [Test]
    public async Task ConfiguratorTest()
    {
        var messageText = "test";
        var services = new ServiceCollection();
        services.AddTelegramBot(x =>
        {
            x.UseTestClient();
            x.ConfigureUpdatePipeline(xx => { xx.UseHandler<ReplyWithMessageHandler>(); });
        });
        var provider = services.BuildServiceProvider();
        var bot = provider.GetRequiredService<IBot>();
        var update = new UpdateBuilder().WithMessage(x => { x.WithText(messageText); }).Build();
        await bot.HandleUpdateAsync(update);
        Assert.IsTrue(new BotRepliesTester(bot).RepliesWithText(0, messageText).IsMatched());
    }
}