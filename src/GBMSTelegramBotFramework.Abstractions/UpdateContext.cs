using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Abstractions;

public abstract class UpdateContext : IDisposable
{
    private readonly Lazy<IDictionary<object, object>> _items = new(new Dictionary<object, object>());
    public BotContext BotContext { get; set; }
    public Update Update { get; set; }
    public IDictionary<object, object> Items => _items.Value;
    public IFeaturesCollection Features { get; set; }
    public IServiceProvider Services { get; set; }
    public abstract IUpdateContextReply Reply { get; }

    protected UpdateContext(IServiceProvider services, Update update, BotContext botContext,
        IFeaturesCollection features)
    {
        Services = services;
        Update = update;
        BotContext = botContext;
        Features = features;
    }

    public abstract void Dispose();
}