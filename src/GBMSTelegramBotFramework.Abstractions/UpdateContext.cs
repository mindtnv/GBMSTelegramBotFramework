using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Abstractions;

public abstract class UpdateContext : IDisposable
{
    private readonly Lazy<IDictionary<object, object>> _items = new(new Dictionary<object, object>());
    public BotContext BotContext { get; set; }
    public Update Update { get; set; }
    public IDictionary<object, object> Items => _items.Value;
    public ICrossRequestContext CrossRequestContext { get; set; }
    public IServiceProvider Services { get; set; }

    protected UpdateContext(IServiceProvider services, Update update, BotContext botContext,
        ICrossRequestContext crossRequestContext)
    {
        Services = services;
        Update = update;
        BotContext = botContext;
        CrossRequestContext = crossRequestContext;
    }

    public abstract void Dispose();
}