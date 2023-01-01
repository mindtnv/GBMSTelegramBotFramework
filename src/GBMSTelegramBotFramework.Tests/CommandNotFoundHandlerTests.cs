using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;
using GBMSTelegramBotFramework.Tests.Commands;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class CommandNotFoundHandlerTests
{
    [Test]
    public async Task ShouldFireNotFound()
    {
        var notFoundText = "Command not found";
        var bot = Utils.CreateTestBot(p =>
        {
            p.UseHandler<SumCommandHandler>();
            p.UseCommandNotFoundHandler(notFoundText);
        });
        var update = UpdateBuilder.WithTextMessage("/start").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText(notFoundText));
    }

    [Test]
    public async Task ShouldNotFireNotFound()
    {
        var notFoundText = "Command not found";
        var bot = Utils.CreateTestBot(p =>
        {
            p.UseHandler<SumCommandHandler>();
            p.UseCommandNotFoundHandler(notFoundText);
        });
        var update = UpdateBuilder.WithTextMessage("Test").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldNotSendMessage());
    }
}