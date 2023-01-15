using System.Collections.Concurrent;
using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class InMemoryChatIdResolverStore : IChatIdResolverStore
{
    private readonly ConcurrentDictionary<string, IChatIdResolver> _resolvers = new(StringComparer.OrdinalIgnoreCase);

    public IChatIdResolver GetResolver(string botName)
    {
        if (!_resolvers.ContainsKey(botName))
            _resolvers[botName] = new InMemoryChatIdResolver();

        return _resolvers[botName];
    }
}