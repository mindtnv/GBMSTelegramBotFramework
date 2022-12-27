namespace GBMSTelegramBotFramework.Abstractions;

public interface IUpdatePipelineBuilder
{
    IUpdatePipelineBuilder Use(Func<UpdateDelegate, UpdateDelegate> middleware);
    UpdateDelegate Build();
}