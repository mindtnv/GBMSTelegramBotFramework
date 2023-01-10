using System.Collections.Concurrent;
using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class InMemoryUserIdResolver : IUserIdResolver
{
    private readonly ConcurrentDictionary<long, long> _chatIdToUserId = new();
    public Task<long> ResolveUserIdAsync(long chatId) => Task.FromResult(_chatIdToUserId[chatId]);

    public Task CorrelateUserIdWithChatId(long userId, long chatId)
    {
        _chatIdToUserId.TryAdd(chatId, userId);
        return Task.CompletedTask;
    }
}