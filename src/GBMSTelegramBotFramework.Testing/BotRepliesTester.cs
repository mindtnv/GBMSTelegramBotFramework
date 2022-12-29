using GBMSTelegramBotFramework.Abstractions;
using Telegram.Bot.Requests.Abstractions;

namespace GBMSTelegramBotFramework.Testing;

public class BotRepliesTester : IBotRepliesTester
{
    private readonly IBot _bot;
    private readonly TestTelegramBotClient _client;
    private bool _isMatched = true;
    public int CurrentRequestIndex { get; private set; }

    public BotRepliesTester(IBot bot)
    {
        _bot = bot;
        _client = _bot.Client as TestTelegramBotClient ??
                  throw new ArgumentException("Client must be a TestTelegramBotClient", nameof(_bot.Client));
    }

    public BotRepliesTester RepliesWith(Func<IRequest, bool> matcher)
    {
        if (CurrentRequestIndex >= _client.Requests.Count)
            throw new IndexOutOfRangeException("No more requests to match");

        _isMatched &= matcher(_client.Requests[CurrentRequestIndex++]);
        return this;
    }

    public bool IsMatched() => _isMatched;
}