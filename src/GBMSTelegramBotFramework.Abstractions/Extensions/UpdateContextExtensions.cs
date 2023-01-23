namespace GBMSTelegramBotFramework.Abstractions.Extensions;

public static class UpdateContextExtensions
{
    public static void StopPipelineExecution(this UpdateContext context)
    {
        context.Cts.Cancel();
    }
}