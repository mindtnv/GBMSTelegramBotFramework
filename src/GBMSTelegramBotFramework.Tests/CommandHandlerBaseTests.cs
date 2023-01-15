using FluentAssertions;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;
using GBMSTelegramBotFramework.Tests.Commands;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class CommandHandlerBaseTests
{
    [SetUp]
    public void SetUp()
    {
        CommandHandlerBase.Prefix = "/";
    }

    [Test]
    public async Task ContextItemsIsCommandExecutedTest1()
    {
        var finalAssertion = (UpdateContext context) =>
        {
            context.Items.Should().ContainKey(CommandHandlerBase.IsCommandExecuted);
            context.Items["IsCommandExecuted"].As<bool>().Should().BeTrue();
            context.Items.Should().ContainKey(CommandHandlerBase.CommandName);
            context.Items["CommandName"].As<string>().Should().Be("start");
        };
        var bot = Utils.CreateTestBot(bot =>
        {
            bot.UseHandler<StartCommandHandler>()
               .UseCommand<SumCommandHandler>()
               .AssertContext(finalAssertion);
        });
        var update = UpdateBuilder.WithTextMessage("/start").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText(StartCommandHandler.Message));
    }

    [Test]
    public async Task ContextItemsIsCommandExecutedTest2()
    {
        var firstAssertion = (UpdateContext context) =>
        {
            context.Items.Should().ContainKey(CommandHandlerBase.IsCommandExecuted);
            context.Items["IsCommandExecuted"].As<bool>().Should().BeFalse();
            context.Items.Should().ContainKey(CommandHandlerBase.CommandName);
            context.Items["CommandName"].As<string>().Should().Be("sum");
        };
        var finalAssertion = (UpdateContext context) =>
        {
            context.Items.Should().ContainKey(CommandHandlerBase.IsCommandExecuted);
            context.Items["IsCommandExecuted"].As<bool>().Should().BeTrue();
            context.Items.Should().ContainKey(CommandHandlerBase.CommandName);
            context.Items["CommandName"].As<string>().Should().Be("sum");
        };

        var bot = Utils.CreateTestBot(bot =>
        {
            bot.UseHandler<StartCommandHandler>()
               .AssertContext(firstAssertion)
               .UseCommand<SumCommandHandler>()
               .AssertContext(finalAssertion);
        });
        var update = UpdateBuilder.WithTextMessage("/sum 1 5").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText("6"));
    }

    [Test]
    public async Task WithArgsTest()
    {
        CommandHandlerBase.Prefix = "";
        var bot = Utils.CreateTestBot(bot => bot.UseHandler<SumCommandHandler>());
        var update = UpdateBuilder.WithTextMessage("sum 5 10").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText("15"));
    }

    [Test]
    public async Task WithoutArgsTest()
    {
        var bot = Utils.CreateTestBot(bot => bot.UseHandler<StartCommandHandler>());
        var update = UpdateBuilder.WithTextMessage("/start").Build();
        await bot.HandleAndAssertAsync(update, x => x.ShouldSendMessageWithText(StartCommandHandler.Message));
    }
}