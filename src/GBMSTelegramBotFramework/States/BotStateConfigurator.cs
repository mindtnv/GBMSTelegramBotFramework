using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.States;

public class BotStateConfigurator : IBotStateConfigurator
{
    private static readonly Func<UpdateContext, Task> EmptyCallback = _ => Task.CompletedTask;
    private readonly UpdatePipelineConfigurator _updatePipelineConfigurator;
    private bool? _isInitialState;
    private string? _name;
    private Func<UpdateContext, Task>? _onEnter;
    private Func<UpdateContext, Task>? _onLeave;
    private IBotState? _state;
    private Type? _stateType;

    public BotStateConfigurator(IBotState state, IServiceCollection services) : this(services)
    {
        _state = state;
    }

    public BotStateConfigurator(IServiceCollection services)
    {
        Services = services;
        _updatePipelineConfigurator = new UpdatePipelineConfigurator(Services);
        On = _updatePipelineConfigurator.On;
    }

    public IServiceCollection Services { get; }
    public IBotOnConfigurator On { get; }

    IUpdatePipelineConfigurator IUpdatePipelineConfigurator.Configure(Action<IUpdatePipelineBuilder> configure) =>
        _updatePipelineConfigurator.Configure(configure);

    void IUpdatePipelineConfigurator.ConfigureBuilder(IUpdatePipelineBuilder builder)
    {
        _updatePipelineConfigurator.ConfigureBuilder(builder);
    }

    public IBotStateConfigurator WithState(IBotState state)
    {
        _state = state;
        return this;
    }

    public IBotStateConfigurator WithState<TState>() where TState : class, IBotState
    {
        _stateType = typeof(TState);
        return this;
    }

    public IBotStateConfigurator WithName(string name)
    {
        _name = name;
        return this;
    }

    public IBotStateConfigurator OnEnter(Func<UpdateContext, Task> callback)
    {
        _onEnter = callback;
        return this;
    }

    public IBotStateConfigurator OnLeave(Func<UpdateContext, Task> callback)
    {
        _onLeave = callback;
        return this;
    }

    public IBotStateConfigurator Initial()
    {
        _isInitialState = true;
        return this;
    }

    public IBotStateConfigurator ConfigureUpdatePipeline(Action<IUpdatePipelineConfigurator> configure)
    {
        configure(this);
        return this;
    }

    public BotStateDefinition BuildBotStateDefinition(IServiceProvider serviceProvider)
    {
        IBotState? stateFromType = null;
        var name = _name;
        var isInitial = _isInitialState;
        var onEnter = _onEnter;
        var onLeave = _onLeave;

        if (_state is not null)
        {
            onEnter = _state.OnEnterAsync;
            onLeave = _state.OnLeaveAsync;
            name = _state.Name;
            isInitial = _state.IsInitial;
        }

        if (_stateType is not null)
        {
            stateFromType = ActivatorUtilities.CreateInstance(serviceProvider, _stateType) as IBotState;
            name = stateFromType!.Name;
            onLeave = stateFromType.OnLeaveAsync;
            onEnter = stateFromType.OnEnterAsync;
            isInitial = stateFromType.IsInitial;
        }

        onEnter ??= EmptyCallback;
        onLeave ??= EmptyCallback;
        isInitial ??= false;
        name = name ?? throw new InvalidOperationException("State name is not set");

        var updatePipelineBuilder = new UpdatePipelineBuilder(serviceProvider);
        updatePipelineBuilder.UseMiddleware(typeof(StopMiddleware));

        if (stateFromType is not null)
            stateFromType.ConfigureUpdatePipeline(_updatePipelineConfigurator);
        else
            _state?.ConfigureUpdatePipeline(_updatePipelineConfigurator);

        _updatePipelineConfigurator.ConfigureBuilder(updatePipelineBuilder);
        var updateDelegate = updatePipelineBuilder.Build();

        return new BotStateDefinition
        {
            Name = name,
            OnEnter = onEnter,
            OnLeave = onLeave,
            UpdateDelegate = updateDelegate,
            IsInitial = isInitial.Value,
        };
    }
}