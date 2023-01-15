namespace GBMSTelegramBotFramework.Abstractions;

public interface IChatIdResolver
{
    Task<long> GetChatIdAsync(long userId);
    Task AddCorrelationAsync(long userId, long chatId);
    Task<bool> ContainsCorrelationAsync(long userId);
}