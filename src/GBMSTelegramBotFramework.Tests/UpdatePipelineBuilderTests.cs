using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
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
        var handler = new UpdatePipelineBuilder().Build();
        await handler(context.Object);
    }

    [Test]
    public async Task ChainingWorkProperlyCounter([Values(10, 20, 30, 40)] int n)
    {
        var context = new Mock<UpdateContext>();
        var builder = new UpdatePipelineBuilder();
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
    public async Task ChainingWorkProperlyItems([Values(10, 20, 30, 40)] int n)
    {
        var context = new Mock<UpdateContext>();
        var builder = new UpdatePipelineBuilder();
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
}