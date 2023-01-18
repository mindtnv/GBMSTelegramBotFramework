namespace GBMSTelegramBotFramework.Abstractions;

public interface IFeaturesCollection
{
    T? Get<T>() where T : class;
    void Set<T>(T item) where T : class;
    void Remove<T>() where T : class;
    bool Contains<T>() where T : class;
}