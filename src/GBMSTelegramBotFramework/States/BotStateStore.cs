using System.Collections.Concurrent;

namespace GBMSTelegramBotFramework.States;

public class BotStateStore : IBotStateStore
{
    private readonly ConcurrentDictionary<string, BotStateDefinition> _states;
    private BotStateDefinition? _initialState;

    public BotStateStore()
    {
        _states = new ConcurrentDictionary<string, BotStateDefinition>(StringComparer.OrdinalIgnoreCase);
    }

    public BotStateStore(IEnumerable<BotStateDefinition> states)
    {
        _states = new ConcurrentDictionary<string, BotStateDefinition>(states.ToDictionary(x => x.Name, x => x),
            StringComparer.OrdinalIgnoreCase);
    }

    public void AddStateDefinition(BotStateDefinition stateDefinition)
    {
        _states.TryAdd(stateDefinition.Name, stateDefinition);
        if (stateDefinition.IsInitial)
            _initialState = stateDefinition;
    }

    public BotStateDefinition GetStateDefinition(string stateName) => _states.TryGetValue(stateName, out var state)
        ? state
        : throw new BotStateNotFoundException(stateName);

    public BotStateDefinition? GetInitialStateDefinition() => _initialState;
}