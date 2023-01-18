namespace GBMSTelegramBotFramework.States;

public interface IBotStateStore
{
    void AddStateDefinition(BotStateDefinition stateDefinition);
    BotStateDefinition GetStateDefinition(string stateName);
    BotStateDefinition? GetInitialStateDefinition();
}