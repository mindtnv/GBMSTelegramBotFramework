namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdateHandler
{
    Task HandleUpdateAsync(UpdateContext context);
}