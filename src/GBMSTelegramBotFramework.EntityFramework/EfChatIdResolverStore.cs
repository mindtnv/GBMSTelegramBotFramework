using System.Collections.Concurrent;
using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.EntityFramework;

public class EfChatIdResolverStore : IChatIdResolverStore
{
    private readonly DbBotContext _context;
    private readonly ConcurrentDictionary<string, IChatIdResolver> _resolvers = new(StringComparer.OrdinalIgnoreCase);

    public EfChatIdResolverStore(DbBotContext context)
    {
        _context = context;
    }

    public IChatIdResolver GetResolver(string botName)
    {
        if (!_resolvers.ContainsKey(botName))
            _resolvers[botName] = new EfChatIdResolver(_context, botName);

        return _resolvers[botName];
    }
}