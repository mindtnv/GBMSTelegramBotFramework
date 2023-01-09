using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class CrossRequestContextStore : ICrossRequestContextStore
{
    private readonly Dictionary<long, ICrossRequestContext> _contexts = new();

    public ICrossRequestContext Get(long userId)
    {
        if (!_contexts.ContainsKey(userId))
            _contexts[userId] = new CrossRequestContext();

        return _contexts[userId];
    }
}