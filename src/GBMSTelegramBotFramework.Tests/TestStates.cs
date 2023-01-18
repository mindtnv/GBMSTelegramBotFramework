using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.States;
using GBMSTelegramBotFramework.States.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class TestStates
{
    [Test]
    public async Task InitialStateTest()
    {
        var bot = Utils.CreateTestBot(bot =>
        {
            bot.WithState(state =>
            {
                state.WithName("test2")
                     .OnEnter(ctx => ctx.SendTextMessageAsync("enter2"))
                     .OnLeave(ctx => ctx.SendTextMessageAsync("leave2"))
                     .On.Text(ctx => ctx.EnterStateAsync("test1"));
            });
            bot.WithState(state =>
            {
                state.WithName("test1")
                     .Initial()
                     .OnEnter(ctx => ctx.SendTextMessageAsync("enter1"))
                     .OnLeave(ctx => ctx.SendTextMessageAsync("leave1"))
                     .On.Text(ctx => ctx.EnterStateAsync("test2"));
            });

            bot.UseMiddleware<StateMiddleware>();
        });
        var update1 = UpdateBuilder.WithTextMessage("test").Build();
        var update2 = UpdateBuilder.WithTextMessage("test").Build();
        await bot.HandleUpdateAsync(update1);
        await bot.HandleUpdateAsync(update2);
        bot.Assert(x =>
        {
            x.ShouldSendMessageWithText("leave1").GoToNextRequest();
            x.ShouldSendMessageWithText("enter2").GoToNextRequest();
            x.ShouldSendMessageWithText("leave2").GoToNextRequest();
            x.ShouldSendMessageWithText("enter1").GoToNextRequest();
        });
    }

    [Test]
    public async Task NoInitialStateTest()
    {
        var bot = Utils.CreateTestBot(bot =>
        {
            bot.WithState(state =>
            {
                state.WithName("test2")
                     .OnEnter(ctx => ctx.SendTextMessageAsync("enter2"))
                     .OnLeave(ctx => ctx.SendTextMessageAsync("leave2"))
                     .On.Text(ctx => ctx.EnterStateAsync("test1"));
            });
            bot.WithState(state =>
            {
                state.WithName("test1")
                     .OnEnter(ctx => ctx.SendTextMessageAsync("enter1"))
                     .OnLeave(ctx => ctx.SendTextMessageAsync("leave1"))
                     .On.Text(ctx => ctx.EnterStateAsync("test2"));
            });
            bot.On.Text(async ctx =>
            {
                if (ctx.Update.Message!.Text == "/start")
                    await ctx.EnterStateAsync("test1");
            });
            bot.UseMiddleware<StateMiddleware>();
        });
        var update1 = UpdateBuilder.WithTextMessage("/start").Build();
        var update2 = UpdateBuilder.WithTextMessage("test").Build();
        var update3 = UpdateBuilder.WithTextMessage("test").Build();
        await bot.HandleUpdateAsync(update1);
        await bot.HandleUpdateAsync(update2);
        await bot.HandleUpdateAsync(update3);
        bot.Assert(x =>
        {
            x.ShouldSendMessageWithText("enter1").GoToNextRequest();
            x.ShouldSendMessageWithText("leave1").GoToNextRequest();
            x.ShouldSendMessageWithText("enter2").GoToNextRequest();
            x.ShouldSendMessageWithText("leave2").GoToNextRequest();
            x.ShouldSendMessageWithText("enter1").GoToNextRequest();
        });
    }
}