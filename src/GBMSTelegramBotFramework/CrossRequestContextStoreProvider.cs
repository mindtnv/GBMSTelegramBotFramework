﻿using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class CrossRequestContextStoreProvider : ICrossRequestContextStoreProvider
{
    private readonly Dictionary<string, ICrossRequestContextStore> _stores = new(StringComparer.OrdinalIgnoreCase);

    public ICrossRequestContextStore Get(string botName)
    {
        if (_stores.TryGetValue(botName, out var store))
            return store;

        store = new CrossRequestContextStore();
        _stores.Add(botName, store);

        return store;
    }
}