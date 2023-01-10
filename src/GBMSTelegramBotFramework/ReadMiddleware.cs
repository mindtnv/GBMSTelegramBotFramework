using GBMSTelegramBotFramework.Abstractions;
using Telegram.Bot.Types.Enums;

namespace GBMSTelegramBotFramework;

public class ReadMiddleware : IUpdateMiddleware
{
    public async Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
    {
        var state = context.CrossRequestContext.Get<ReadMiddlewareState>();
        if (state == null || context.Update.Type != UpdateType.Message)
        {
            await next(context);
            return;
        }

        context.CrossRequestContext.Remove<ReadMiddlewareState>();
        state.TaskCompletionSource.SetResult(context.Update.Message!.Text!);
        await state.TaskCompletionSource.Task.ConfigureAwait(false);
    }

    public class ReadMiddlewareState
    {
        public TaskCompletionSource<string> TaskCompletionSource { get; set; }
    }
}