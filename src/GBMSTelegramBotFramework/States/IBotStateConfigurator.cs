using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States;

public interface IBotStateConfigurator : IUpdatePipelineConfigurator
{
    IBotStateConfigurator WithState<TState>() where TState : class, IBotStateDescriptor;
    IBotStateConfigurator WithState(IBotStateDescriptor stateDescriptor);
    IBotStateConfigurator WithName(string name);
    IBotStateConfigurator OnEnter(Func<UpdateContext, Task> callback);
    IBotStateConfigurator OnLeave(Func<UpdateContext, Task> callback);
    IBotStateConfigurator Initial();
    IBotStateConfigurator ConfigureUpdatePipeline(Action<IUpdatePipelineConfigurator> configure);
    BotState BuildBotStateDefinition(IServiceProvider serviceProvider);
}