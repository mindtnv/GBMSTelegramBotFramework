using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States;

public interface IBotState
{
    string Name { get; }
    bool IsInitial { get; }
    void ConfigureUpdatePipeline(IUpdatePipelineConfigurator configurator);
    Task OnEnterAsync(UpdateContext context);
    Task OnLeaveAsync(UpdateContext context);
}