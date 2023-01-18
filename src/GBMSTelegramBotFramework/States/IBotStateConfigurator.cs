using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States;

public interface IBotStateConfigurator : IUpdatePipelineConfigurator
{
    IBotStateConfigurator WithState<TState>() where TState : class, IBotState;
    IBotStateConfigurator WithState(IBotState state);
    IBotStateConfigurator WithName(string name);
    IBotStateConfigurator OnEnter(Func<UpdateContext, Task> callback);
    IBotStateConfigurator OnLeave(Func<UpdateContext, Task> callback);
    IBotStateConfigurator Initial();
    IBotStateConfigurator ConfigureUpdatePipeline(Action<IUpdatePipelineConfigurator> configure);
    BotStateDefinition BuildBotStateDefinition(IServiceProvider serviceProvider);
}