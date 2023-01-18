using System.Collections.Concurrent;
using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class UpdateContextFeaturesCollectionStore : IUpdateContextFeaturesCollectionStore
{
    private readonly ConcurrentDictionary<long, IFeaturesCollection> _collections = new();
    
    public IFeaturesCollection GetFeaturesCollection(long id)
    {
        if (!_collections.ContainsKey(id))
            _collections[id] = new FeaturesCollection();

        return _collections[id];
    }
}