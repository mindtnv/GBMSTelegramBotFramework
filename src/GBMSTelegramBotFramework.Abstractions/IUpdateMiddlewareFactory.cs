namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdateMiddlewareFactory
{
    IUpdateMiddleware Create(Type updateMiddlewareType);
}