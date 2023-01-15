namespace GBMSTelegramBotFramework.Abstractions;

public interface IChatIdResolverStore
{
    IChatIdResolver GetResolver(string botName);
}