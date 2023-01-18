using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Tests;

public class UpdatePipelineBuilderTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task DontFallWithEmptyPipeline()
    {
        var context = new Mock<TestingUpdateContext>();
        var provider = new Mock<IServiceProvider>();
        var handler = new UpdatePipelineBuilder(provider.Object).Build();
        await handler(context.Object);
    }

    [Test]
    public async Task UseTest([Random(5, 50, 5)] int n)
    {
        var context = new Mock<TestingUpdateContext>();
        var provider = new Mock<IServiceProvider>();
        var builder = new UpdatePipelineBuilder(provider.Object);
        var counter = 0;
        for (var i = 0; i < n; i++)
            builder.Use((ctx, next) =>
            {
                counter++;
                return next(ctx);
            });

        var handler = builder.Build();
        await handler(context.Object);
        Assert.That(counter, Is.EqualTo(n));
    }

    [Test]
    public async Task UseFuncMiddlewareTest([Random(5, 50, 5)] int n)
    {
        var context = new Mock<TestingUpdateContext>();
        var provider = new Mock<IServiceProvider>();
        var builder = new UpdatePipelineBuilder(provider.Object);
        var arr = new bool[n];
        context.Object.Items["i"] = 0;
        for (var j = 0; j < n; j++)
            builder.Use((ctx, next) =>
            {
                var i = (int) ctx.Items["i"];
                arr[i] = true;

                if (i != 0)
                    Assert.IsTrue(arr[i - 1]);
                if (i != n - 1)
                    Assert.IsFalse(arr[i + 1]);

                ctx.Items["i"] = i + 1;
                return next(ctx);
            });

        var handler = builder.Build();
        await handler(context.Object);
        Assert.That(arr.All(x => x), Is.True);
    }

    [Test]
    public async Task UseMiddlewareTest([Random(5, 50, 5)] int n)
    {
        var services = new ServiceCollection();
        services.AddSingleton<TestingCounter>();
        services.AddSingleton<IUpdateMiddlewareFactory, UpdateMiddlewareFactory>();
        services.AddTransient<TestMiddleware>();
        var provider = services.BuildServiceProvider();
        var context = new TestingUpdateContext(provider, new Mock<Update>().Object,
            new Mock<BotContext>().Object, new Mock<IFeaturesCollection>().Object);
        var builder = new UpdatePipelineBuilder(provider);
        builder.UseMiddleware(typeof(TestMiddleware)).UseMiddleware<TestMiddleware>();
        for (var i = 0; i < n; i++)
            builder.UseMiddleware(typeof(TestMiddleware));
        var handler = builder.Build();
        await handler(context);
        var counter = provider.GetRequiredService<TestingCounter>();
        Assert.That(counter.Value, Is.EqualTo(2 + n));
    }

    [Test]
    public async Task UseHandlerTest([Random(5, 50, 5)] int n)
    {
        var services = new ServiceCollection();
        services.TryAddSingleton<IUpdateMiddlewareFactory, UpdateMiddlewareFactory>();
        services.TryAddSingleton<IUpdateHandlerFactory, UpdateHandlerFactory>();
        services.TryAddSingleton<IUpdateContextFactory, UpdateContextFactory>();
        services.TryAdd(ServiceDescriptor.Transient(typeof(UpdateHandlerMiddleware<>),
            typeof(UpdateHandlerMiddleware<>)));
        services.AddSingleton<TestingCounter>();
        services.AddTransient<TestHandler>();
        var provider = services.BuildServiceProvider();
        var context = new TestingUpdateContext(provider, new Mock<Update>().Object,
            new Mock<BotContext>().Object, new Mock<IFeaturesCollection>().Object);
        var builder = new UpdatePipelineBuilder(provider);
        for (var i = 0; i < n; i++)
            builder.UseHandler<TestHandler>();
        var handler = builder.Build();
        await handler(context);
        var counter = provider.GetRequiredService<TestingCounter>();
        Assert.That(counter.Value, Is.EqualTo(n));
    }

    private class TestMiddleware : IUpdateMiddleware
    {
        private readonly TestingCounter _testingCounter;

        public TestMiddleware(TestingCounter testingCounter)
        {
            _testingCounter = testingCounter;
        }

        public Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
        {
            _testingCounter.Value++;
            return next(context);
        }
    }

    private class TestHandler : IUpdateHandler
    {
        private readonly TestingCounter _testingCounter;

        public TestHandler(TestingCounter testingCounter)
        {
            _testingCounter = testingCounter;
        }

        public Task HandleUpdateAsync(UpdateContext context)
        {
            _testingCounter.Value++;
            return Task.CompletedTask;
        }
    }
}