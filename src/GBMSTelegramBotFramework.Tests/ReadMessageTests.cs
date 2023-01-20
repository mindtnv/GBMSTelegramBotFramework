using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Commands.Extensions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;
using GBMSTelegramBotFramework.Tests.Commands.Examples;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class ReadMessageTests
{
    [Test]
    public async Task SumReadCommandTest([Random(-1000, 1000, 1)] int a, [Random(-1000, 1000, 1)] int b)
    {
        var bot = Utils.CreateTestBot(bot =>
        {
            bot.UseReadMiddleware();
            bot.WithCommand<SumReadCommand>();
            bot.UseCommands();
        });
        var update = UpdateBuilder.WithTextMessage("/sum").Build();
        var updateA = UpdateBuilder.WithTextMessage(a.ToString()).Build();
        var updateB = UpdateBuilder.WithTextMessage(b.ToString()).Build();
        await bot.HandleUpdateAsync(update);
        await bot.HandleUpdateAsync(updateA);
        await bot.HandleUpdateAsync(updateB);
        bot.Assert(x => x.ShouldSendMessageWithText((a + b).ToString()));
    }
}