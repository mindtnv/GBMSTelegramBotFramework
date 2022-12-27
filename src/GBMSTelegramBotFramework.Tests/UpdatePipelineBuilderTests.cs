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
    public async Task ChainingWorkProperly([Values(10, 20, 30, 40)] int n)
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
}