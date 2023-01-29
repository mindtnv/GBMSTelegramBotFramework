using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class StopPipelineExecutionTests
{
    [Test]
    public async Task StopPipelineExecutionTest()
    {
        var bot = Utils.CreateTestBot(bot =>
        {
            bot.On.Text("/test", ctx =>
            {
                ctx.StopPipelineExecution();
                return ctx.Reply.WithText("test1");
            });
            bot.On.Text("/test", ctx => ctx.Reply.WithText("test2"));
        });
        var update = UpdateBuilder.WithTextMessage("/test").Build();
        await bot.HandleAndAssertAsync(update,
            x =>
                x.ShouldSendMessageWithText("test1")
                 .GoToNextRequest()
                 .ShouldHaveNotMessage()
        );
    }
}