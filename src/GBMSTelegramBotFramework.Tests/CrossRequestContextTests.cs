using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;
using GBMSTelegramBotFramework.Tests.Commands;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class CrossRequestContextTests
{
    [Test]
    public async Task CrossRequestContextIntegratedTest([Random(5, 50, 5)] int n)
    {
        var bot = Utils.CreateTestBot(bot => bot.UseCommand<IncrementCommand>());
        for (var i = 0; i < n; i++)
        {
            var update = UpdateBuilder.WithTextMessage("/increment").Build();
            await bot.HandleUpdateAsync(update);
        }

        bot.Assert(x =>
        {
            for (var i = 0; i < n; i++)
                x.ShouldSendMessageWithText((i + 1).ToString()).GoToNextRequest();
        });
    }

    [Test]
    public void CrossRequestContextStoreTest()
    {
        var store = new CrossRequestContextStore();
        var context1 = store.Get(1);
        context1.Set(new TestingCounter());
        var context2 = store.Get(1);
        var counter1 = context2.Get<TestingCounter>();
        counter1!.Increment();
        counter1.Increment();
        counter1.Increment();
        var context3 = store.Get(1);
        var counter2 = context3.Get<TestingCounter>();
        Assert.That(counter2!.Value, Is.EqualTo(3));
    }
}