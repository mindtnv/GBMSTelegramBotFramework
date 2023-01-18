using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States;

public abstract class BotStateBase : IBotState
{
    public abstract string Name { get; }
    public virtual bool IsInitial => false;
    public abstract void ConfigureUpdatePipeline(IUpdatePipelineConfigurator configurator);
    public virtual Task OnEnterAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnLeaveAsync(UpdateContext context) => Task.CompletedTask;
}