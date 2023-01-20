using FluentAssertions;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Commands;
using GBMSTelegramBotFramework.Commands.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Tests.Commands;

[TestFixture(Category = "Commands")]
public class CommandMiddlewareTests
{
    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        services.AddTransient<IUpdateMiddlewareFactory, UpdateMiddlewareFactory>();
        var configurator = new UpdatePipelineConfigurator(services, new FeaturesCollection());
        configurator.WithCommand<TestCommand>();
        configurator.UseCommands();
        _provider = services.BuildServiceProvider();
        var builder = new UpdatePipelineBuilder(_provider);
        configurator.ConfigureBuilder(builder);
        _pipeline = builder.Build();
    }

    private IServiceProvider _provider = null!;
    private UpdateDelegate _pipeline = null!;

    private class TestCommand : CommandBase<TestCommand>
    {
        public static readonly string ExecutedKey = "Executed";

        public override Task ExecuteAsync(UpdateContext context, string[] args)
        {
            context.Items[ExecutedKey] = string.Join(" ", args);
            return Task.CompletedTask;
        }

        public override void ConfigureDescriptor(ICommandOptionsBuilder builder)
        {
            builder.WithAliases("/test", "test", "te st");
        }
    }

    [Test]
    public async Task CommandFromMessageWithEmptySpacesTestWithArgs()
    {
        var update = new Update
        {
            Message = new Message
            {
                Text = "te st  4 5 8 11 23  3",
            },
        };
        var context = new TestingUpdateContext(_provider, update, new Mock<BotContext>().Object,
            new Mock<IFeaturesCollection>().Object);
        await _pipeline(context);
        context.Items.Should()
               .ContainKey(TestCommand.ExecutedKey)
               .WhoseValue.As<string>()
               .Should()
               .Be("4 5 8 11 23 3");
        context.Items.Should()
               .ContainKey(CommandMiddleware.CommandNameKey)
               .WhoseValue.As<string>()
               .Should()
               .Be("te st");
    }

    [Test]
    public async Task CommandFromMessageWithEmptySpacesTestWithoutArgs()
    {
        var update = new Update
        {
            Message = new Message
            {
                Text = "te st",
            },
        };
        var context = new TestingUpdateContext(_provider, update, new Mock<BotContext>().Object,
            new Mock<IFeaturesCollection>().Object);
        await _pipeline(context);
        context.Items.Should()
               .ContainKey(TestCommand.ExecutedKey)
               .WhoseValue.As<string>()
               .Should()
               .BeEmpty();
        context.Items.Should()
               .ContainKey(CommandMiddleware.CommandNameKey)
               .WhoseValue.As<string>()
               .Should()
               .Be("te st");
    }

    [Test]
    public async Task CommandFromMessageWithoutSpacesWithArgsTest()
    {
        var update = new Update
        {
            Message = new Message
            {
                Text = "/test 12 22 4   5",
            },
        };
        var context = new TestingUpdateContext(_provider, update, new Mock<BotContext>().Object,
            new Mock<IFeaturesCollection>().Object);
        await _pipeline(context);
        context.Items.Should()
               .ContainKey(TestCommand.ExecutedKey)
               .WhoseValue.As<string>()
               .Should()
               .Be("12 22 4 5");
        context.Items.Should()
               .ContainKey(CommandMiddleware.CommandNameKey)
               .WhoseValue.As<string>()
               .Should()
               .Be("/test");
    }

    [Test]
    public async Task CommandFromMessageWithoutSpacesWithoutArgsTest()
    {
        var update = new Update
        {
            Message = new Message
            {
                Text = "/test",
            },
        };
        var context = new TestingUpdateContext(_provider, update, new Mock<BotContext>().Object,
            new Mock<IFeaturesCollection>().Object);
        await _pipeline(context);
        context.Items.Should()
               .ContainKey(TestCommand.ExecutedKey)
               .WhoseValue.As<string>()
               .Should()
               .BeEmpty();
        context.Items.Should()
               .ContainKey(CommandMiddleware.CommandNameKey)
               .WhoseValue.As<string>()
               .Should()
               .Be("/test");
    }
}