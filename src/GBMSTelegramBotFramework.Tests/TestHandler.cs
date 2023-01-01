using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Tests;

internal class TestHandler : UpdateHandlerBase
{
    public override Task OnMessageAsync(UpdateContext context)
    {
        context.Items["Text"] = context.Update.Message!.Text!;
        return Task.CompletedTask;
    }
}