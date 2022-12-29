using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Abstractions;

public class UpdateContext
{
    private readonly Lazy<IDictionary<object, object>> _items = new(new Dictionary<object, object>());
    public IBot Bot { get; set; }
    public Update Update { get; set; }
    public IDictionary<object, object> Items => _items.Value;
}