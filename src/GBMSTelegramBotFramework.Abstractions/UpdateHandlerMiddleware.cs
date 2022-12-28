namespace GBMSTelegramBotFramework.Abstractions;

public class UpdateHandlerMiddleware<THandler> : IUpdateMiddleware where THandler : IUpdateHandler
{
    private readonly IUpdateHandlerFactory _factory;

    public UpdateHandlerMiddleware(IUpdateHandlerFactory factory)
    {
        _factory = factory;
    }

    public async Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
    {
        var handler = _factory.Create(typeof(THandler));
        await handler.HandleUpdateAsync(context);
        await next(context);
    }
}