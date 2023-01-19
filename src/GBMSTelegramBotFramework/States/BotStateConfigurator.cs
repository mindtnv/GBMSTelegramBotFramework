using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Extensions;
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

    public BotStateConfigurator(IServiceCollection services, IFeaturesCollection botFeatures)
    {
        Services = services;
        BotFeatures = botFeatures;
        _updatePipelineConfigurator = new UpdatePipelineConfigurator(Services, botFeatures);
        _updatePipelineConfigurator.UseMiddleware<StopMiddleware>();
        On = _updatePipelineConfigurator.On;
    }

    public IFeaturesCollection BotFeatures { get; }
    public IServiceCollection Services { get; }
    public IUpdatePipelineOnConfigurator On { get; }

    IUpdatePipelineConfigurator IUpdatePipelineConfigurator.Configure(Action<IUpdatePipelineBuilder> configure) =>
        _updatePipelineConfigurator.Configure(configure);

    void IUpdatePipelineConfigurator.ConfigureBuilder(IUpdatePipelineBuilder builder)
    {
        _updatePipelineConfigurator.ConfigureBuilder(builder);
    }

    public IBotStateConfigurator WithState(IBotState state)
    {
        _onEnter = state.OnEnterAsync;
        _onLeave = state.OnLeaveAsync;
        _name = state.Name;
        _isInitialState = state.IsInitial;
        state.ConfigureUpdatePipeline(_updatePipelineConfigurator);
        return this;
    }

    public IBotStateConfigurator WithState<TState>() where TState : class, IBotState
    {
        var serviceProvider = Services.BuildServiceProvider();
        var state = (ActivatorUtilities.CreateInstance(serviceProvider, typeof(TState)) as IBotState)!;
        _name = state.Name;
        _onLeave = state.OnLeaveAsync;
        _onEnter = state.OnEnterAsync;
        _isInitialState = state.IsInitial;
        state.ConfigureUpdatePipeline(_updatePipelineConfigurator);
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
        var name = _name ?? throw new InvalidOperationException("State name is not set");
        ;
        var isInitial = _isInitialState ?? false;
        var onEnter = _onEnter ?? EmptyCallback;
        var onLeave = _onLeave ?? EmptyCallback;
        var updatePipelineBuilder = new UpdatePipelineBuilder(serviceProvider);
        _updatePipelineConfigurator.ConfigureBuilder(updatePipelineBuilder);
        var updateDelegate = updatePipelineBuilder.Build();

        return new BotStateDefinition
        {
            Name = name,
            OnEnter = onEnter,
            OnLeave = onLeave,
            UpdateDelegate = updateDelegate,
            IsInitial = isInitial,
        };
    }
}