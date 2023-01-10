using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Extensions;
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
            x.ConfigureOptions(o => o.WithName(""));
            x.UseTestingClient();
            x.ConfigureUpdatePipeline(xx => { xx.UseHandler<ReplyWithMessageHandler>(); });
        });
        var provider = services.BuildServiceProvider();
        var bot = provider.GetRequiredService<IBot>();
        var update = UpdateBuilder.WithTextMessage(messageText).Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText(messageText));
    }
}