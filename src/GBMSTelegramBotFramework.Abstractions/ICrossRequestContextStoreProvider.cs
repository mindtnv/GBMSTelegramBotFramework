namespace GBMSTelegramBotFramework.Abstractions;

public interface ICrossRequestContextStoreProvider
{
    ICrossRequestContextStore Get(string botName);
}