using System.Collections.Concurrent;
using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class InMemoryChatIdResolver : IChatIdResolver
{
    private readonly ConcurrentDictionary<long, long> _chatIdToUserId = new();
    public Task<long> GetChatIdAsync(long userId) => Task.FromResult(_chatIdToUserId[userId]);

    public Task AddCorrelationAsync(long userId, long chatId)
    {
        _chatIdToUserId.TryAdd(userId, chatId);
        return Task.CompletedTask;
    }

    public Task<bool> ContainsCorrelationAsync(long userId) => Task.FromResult(_chatIdToUserId.ContainsKey(userId));
}