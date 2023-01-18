using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States.Extensions;

public static class UpdateContextExtensions
{
    public static async Task EnterStateAsync(this UpdateContext updateContext, string stateName)
    {
        var botState = updateContext.Features.Get<StateMiddleware.StateMiddlewareState>();
        var stateStore = updateContext.BotContext.Features.Get<IBotStateStore>() ??
                         throw new InvalidOperationException(
                             "You must register at least one state to use this feature");
        var nextDefinition = stateStore.GetStateDefinition(stateName) ?? throw new BotStateNotFoundException(stateName);
        if (botState != null)
        {
            var currentDefinition = botState.CurrentState;
            await currentDefinition.OnLeave(updateContext);
        }
        else
        {
            botState = new StateMiddleware.StateMiddlewareState();
            updateContext.Features.Set(botState);
        }

        botState.CurrentState = nextDefinition;
        await nextDefinition.OnEnter(updateContext);
    }

    public static BotStateDefinition? GetCurrentState(this UpdateContext updateContext)
    {
        var botState = updateContext.Features.Get<StateMiddleware.StateMiddlewareState>();
        return botState?.CurrentState ?? null;
    }

    public static string? GetCurrentStateName(this UpdateContext updateContext) =>
        updateContext.GetCurrentState()?.Name;
}