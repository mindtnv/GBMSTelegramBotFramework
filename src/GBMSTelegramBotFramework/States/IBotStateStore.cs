namespace GBMSTelegramBotFramework.States;

public interface IBotStateStore
{
    void AddState(BotState state);
    BotState GetState(string stateName);
    BotState? GetInitialState();
}