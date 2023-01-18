using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States;

public class StateMiddleware : IUpdateMiddleware
{
    public async Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
    {
        var state = context.Features.Get<StateMiddlewareState>();
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
            context.Features.Set(state);
        }

        await state.CurrentState.UpdateDelegate(context);
        await next(context);
    }

    public class StateMiddlewareState
    {
        public BotStateDefinition CurrentState { get; set; }
    }
}