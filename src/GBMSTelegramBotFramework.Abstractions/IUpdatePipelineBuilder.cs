namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdatePipelineBuilder
{
    IServiceProvider Services { get; }
    IUpdatePipelineBuilder Use(Func<UpdateDelegate, UpdateDelegate> middleware);
    UpdateDelegate Build();
}