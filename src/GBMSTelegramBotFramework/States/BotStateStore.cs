using System.Collections.Concurrent;

namespace GBMSTelegramBotFramework.States;

public class BotStateStore : IBotStateStore
{
    private readonly ConcurrentDictionary<string, BotState> _states;
    private BotState? _initialState;

    public BotStateStore()
    {
        _states = new ConcurrentDictionary<string, BotState>(StringComparer.OrdinalIgnoreCase);
    }

    public BotStateStore(IEnumerable<BotState> states)
    {
        _states = new ConcurrentDictionary<string, BotState>(states.ToDictionary(x => x.Name, x => x),
            StringComparer.OrdinalIgnoreCase);
    }

    public void AddState(BotState state)
    {
        _states.TryAdd(state.Name, state);
        if (state.IsInitial)
            _initialState = state;
    }

    public BotState GetState(string stateName) => _states.TryGetValue(stateName, out var state)
        ? state
        : throw new BotStateNotFoundException(stateName);

    public BotState? GetInitialState() => _initialState;
}