using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States;

public class StateMiddleware : IUpdateMiddleware
{
    public async Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
    {
        var state = context.CrossRequestContext.Get<StateMiddlewareState>();
        if (state == null)
        {
            var initialBotState = context.BotContext.Features.Get<IBotStateStore>()!.GetInitialStateDefinition();
            if (initialBotState == null)
            {
                await next(context);
                return;
            }

            state = new StateMiddlewareState
            {
                CurrentState = initialBotState,
            };
            context.CrossRequestContext.Set(state);
        }

        await state.CurrentState.UpdateDelegate(context);
        await next(context);
    }

    public class StateMiddlewareState
    {
        public BotStateDefinition CurrentState { get; set; }
    }
}