namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdateHandlerFactory
{
    IUpdateHandler Create(Type updateHandlerType);
}