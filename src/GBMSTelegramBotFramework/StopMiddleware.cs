using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class StopMiddleware : IUpdateMiddleware
{
    public static string StoppingCancellationTokenSource = "_SCTS";

    public async Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;
        var tsc = new TaskCompletionSource<bool>();
        token.Register(() => tsc.SetResult(true));
        context.Items[StoppingCancellationTokenSource] = cts;
        var t = next(context);
        await Task.WhenAny(t, tsc.Task);
        if (t is {IsFaulted: true, Exception: { }})
            throw t.Exception;
    }
}