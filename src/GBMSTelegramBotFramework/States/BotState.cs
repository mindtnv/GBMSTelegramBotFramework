using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States;

public class BotState
{
    public string Name { get; set; }
    public UpdateDelegate UpdateDelegate { get; set; }
    public Func<UpdateContext, Task> OnEnter { get; set; }
    public Func<UpdateContext, Task> OnLeave { get; set; }
    public bool IsInitial { get; set; }
}