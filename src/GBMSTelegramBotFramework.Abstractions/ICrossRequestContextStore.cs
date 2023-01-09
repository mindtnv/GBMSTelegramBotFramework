namespace GBMSTelegramBotFramework.Abstractions;

public interface ICrossRequestContextStore
{
    ICrossRequestContext Get(long userId);
}