using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States;

public class StateMiddleware : IUpdateMiddleware
{
    public async Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
    {
        var state = context.Features.Get<StateMiddlewareState>();
        var stateStore = context.BotContext.Features.Get<IBotStateStore>();
        if (stateStore == null)
        {
            await next(context);
            return;
        }

        if (state == null)
        {
            var initialBotState = stateStore.GetInitialState();
            if (initialBotState == null)
            {
                await next(context);
                return;
            }

            state = new StateMiddlewareState
            {
                CurrentState = initialBotState,
            };
            context.Features.Set(state);
        }

        await state.CurrentState.UpdateDelegate(context);
        await next(context);
    }

    public class StateMiddlewareState
    {
        public BotState CurrentState { get; set; }
    }
}