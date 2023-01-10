using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Extensions;

public static class UpdateContextExtensions
{
    public static Task<string> ReadMessageAsync(this UpdateContext context)
    {
        var tcs = new TaskCompletionSource<string>();
        context.CrossRequestContext.Set(new ReadMiddleware.ReadMiddlewareState
        {
            TaskCompletionSource = tcs,
        });
        context.StopExecution();
        return tcs.Task;
    }

    public static void StopExecution(this UpdateContext context)
    {
        var cts = context.Items[StopMiddleware.StoppingCancellationTokenSource] as CancellationTokenSource;
        cts!.Cancel();
    }
}