using FluentAssertions;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Commands;
using GBMSTelegramBotFramework.Commands.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;
using GBMSTelegramBotFramework.Tests.Commands.Examples;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture(Category = "Commands")]
public class CommandHandlerBaseTests
{
    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public async Task ContextItemsIsCommandExecutedTest1()
    {
        var finalAssertion = (UpdateContext context) =>
        {
            context.Items.Should().ContainKey(CommandsHandler.CommandNameKey);
            context.Items[CommandsHandler.CommandNameKey].As<string>().Should().Be("/start");
        };
        var bot = Utils.CreateTestBot(bot =>
        {
            bot.WithCommand<StartCommand>()
               .WithCommand<SumCommand>();
            bot.UseCommands().AssertContext(finalAssertion);
        });
        var update = UpdateBuilder.WithTextMessage("/start").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText(StartCommand.Message));
    }

    [Test]
    public async Task WithArgsTest()
    {
        var bot = Utils.CreateTestBot(bot =>
        {
            bot.WithCommand<SumCommand>();
            bot.UseCommands();
        });
        var update = UpdateBuilder.WithTextMessage("/sum 5 10").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText("15"));
    }

    [Test]
    public async Task WithoutArgsTest()
    {
        var bot = Utils.CreateTestBot(bot =>
        {
            bot.WithCommand<StartCommand>();
            bot.UseCommands();
        });
        var update = UpdateBuilder.WithTextMessage("/start").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText(StartCommand.Message));
    }
}