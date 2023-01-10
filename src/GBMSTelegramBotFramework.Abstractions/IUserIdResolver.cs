namespace GBMSTelegramBotFramework.Abstractions;

public interface IUserIdResolver
{
    Task<long> ResolveUserIdAsync(long chatId);
    Task CorrelateUserIdWithChatId(long userId, long chatId);
}