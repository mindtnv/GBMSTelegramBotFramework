using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class CrossRequestContext : ICrossRequestContext
{
    private readonly Dictionary<Type, object> _items = new();

    public T? Get<T>() where T : class
    {
        if (!_items.ContainsKey(typeof(T)))
            return default;

        return _items[typeof(T)] as T;
    }

    public void Set<T>(T item) where T : class
    {
        _items[typeof(T)] = item ?? throw new ArgumentNullException(nameof(item));
    }

    public void Remove<T>() where T : class
    {
        _items.Remove(typeof(T));
    }
}