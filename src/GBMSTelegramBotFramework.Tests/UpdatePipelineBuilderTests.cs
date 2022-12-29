using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

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
        var context = new Mock<UpdateContext>();
        var provider = new Mock<IServiceProvider>();
        var handler = new UpdatePipelineBuilder(provider.Object).Build();
        await handler(context.Object);
    }

    [Test]
    public async Task UseTest([Random(5, 50, 5)] int n)
    {
        var context = new Mock<UpdateContext>();
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
        Assert.AreEqual(n, counter);
    }

    [Test]
    public async Task UseFuncMiddlewareTest([Random(5, 50, 5)] int n)
    {
        var context = new Mock<UpdateContext>();
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
        Assert.IsTrue(arr.All(x => x));
    }

    [Test]
    public async Task UseMiddlewareTest([Random(5, 50, 5)] int n)
    {
        var context = new Mock<UpdateContext>();
        var services = new ServiceCollection();
        services.AddSingleton<Counter>();
        services.AddSingleton<IUpdateMiddlewareFactory, UpdateMiddlewareFactory>();
        services.AddTransient<TestMiddleware>();
        var provider = services.BuildServiceProvider();
        var builder = new UpdatePipelineBuilder(provider);
        builder.UseMiddleware(typeof(TestMiddleware)).UseMiddleware<TestMiddleware>();
        for (var i = 0; i < n; i++)
            builder.UseMiddleware(typeof(TestMiddleware));
        var handler = builder.Build();
        await handler(context.Object);
        var counter = provider.GetRequiredService<Counter>();
        Assert.AreEqual(2 + n, counter.Value);
    }

    [Test]
    public async Task UseHandlerTest([Random(5, 50, 5)] int n)
    {
        var context = new Mock<UpdateContext>();
        var services = new ServiceCollection();
        services.TryAddSingleton<IUpdateMiddlewareFactory, UpdateMiddlewareFactory>();
        services.TryAddSingleton<IUpdateHandlerFactory, UpdateHandlerFactory>();
        services.TryAddSingleton<IUpdateContextFactory, UpdateContextFactory>();
        services.TryAdd(ServiceDescriptor.Transient(typeof(UpdateHandlerMiddleware<>),
            typeof(UpdateHandlerMiddleware<>)));
        services.AddSingleton<Counter>();
        services.AddTransient<TestHandler>();
        var provider = services.BuildServiceProvider();
        var builder = new UpdatePipelineBuilder(provider);
        for (var i = 0; i < n; i++)
            builder.UseHandler<TestHandler>();
        var handler = builder.Build();
        await handler(context.Object);
        var counter = provider.GetRequiredService<Counter>();
        Assert.AreEqual(n, counter.Value);
    }

    private class Counter
    {
        public int Value { get; set; }
    }

    private class TestMiddleware : IUpdateMiddleware
    {
        private readonly Counter _counter;

        public TestMiddleware(Counter counter)
        {
            _counter = counter;
        }

        public Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
        {
            _counter.Value++;
            return next(context);
        }
    }

    private class TestHandler : IUpdateHandler
    {
        private readonly Counter _counter;

        public TestHandler(Counter counter)
        {
            _counter = counter;
        }

        public Task HandleUpdateAsync(UpdateContext context)
        {
            _counter.Value++;
            return Task.CompletedTask;
        }
    }
}