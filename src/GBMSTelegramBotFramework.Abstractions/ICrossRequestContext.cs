namespace GBMSTelegramBotFramework.Abstractions;

public interface ICrossRequestContext
{
    T? Get<T>() where T : class;
    void Set<T>(T item) where T : class;
}