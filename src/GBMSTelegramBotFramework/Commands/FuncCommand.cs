using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Commands;

public class FuncCommand : ICommand
{
    private readonly Func<UpdateContext, string[], Task> _handler;

    public FuncCommand(Func<UpdateContext, string[], Task> handler)
    {
        _handler = handler;
    }

    public Task ExecuteAsync(UpdateContext context, string[] args) => _handler(context, args);
}