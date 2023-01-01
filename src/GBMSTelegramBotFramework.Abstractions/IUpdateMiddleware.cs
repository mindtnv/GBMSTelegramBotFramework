namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdateMiddleware
{
    Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next);
}