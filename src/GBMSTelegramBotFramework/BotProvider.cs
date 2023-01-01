using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class BotProvider : IBotProvider
{
    private readonly Dictionary<string, IBot> _botsMap;

    public BotProvider(IEnumerable<IBot> bots)
    {
        _botsMap = bots.ToDictionary(x =>
            x.Options.Name ?? throw new InvalidOperationException("Bot name cannot be null"));
    }

    public IBot GetBot(string botName) =>
        !_botsMap.ContainsKey(botName)
            ? throw new KeyNotFoundException($"Bot with name {botName} not found")
            : _botsMap[botName];
}