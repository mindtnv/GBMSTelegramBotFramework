using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class UpdateHandlerBaseTests
{
    [SetUp]
    public void SetUp()
    {
        _bot = new Mock<IBot>().Object;
        _services = new ServiceCollection();
        _services.AddTelegramBot();
    }

    private IBot _bot = null!;
    private IServiceCollection _services = null!;

    private class TestHandler : UpdateHandlerBase
    {
        public override Task OnMessageAsync(UpdateContext context)
        {
            context.Items["Text"] = context.Update.Message!.Text!;
            return Task.CompletedTask;
        }
    }

    [Test]
    public async Task OnMessageTest()
    {
        var messageText = "test";
        _services.AddSingleton<TestHandler>();
        var provider = _services.BuildServiceProvider();
        var builder = new UpdatePipelineBuilder(provider);
        builder.UseHandler<TestHandler>();
        var handler = builder.Build();
        var update = new UpdateBuilder().WithId(1)
                                        .WithMessage(x => { x.WithText(messageText); })
                                        .Build();
        var context = await provider.GetRequiredService<IUpdateContextFactory>().CreateAsync(_bot, update);
        await handler(context);
        Assert.AreEqual(messageText, context.Items["Text"]);
    }
}