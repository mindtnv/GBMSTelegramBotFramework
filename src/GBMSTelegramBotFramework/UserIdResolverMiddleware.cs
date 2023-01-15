using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;

namespace GBMSTelegramBotFramework;

public class UserIdResolverMiddleware : IUpdateMiddleware
{
    private readonly IChatIdResolverStore _resolverStore;

    public UserIdResolverMiddleware(IChatIdResolverStore resolverStore)
    {
        _resolverStore = resolverStore;
    }

    public async Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
    {
        var resolver =
            _resolverStore.GetResolver(context.Bot.Options.Name ?? throw new InvalidCastException("Bot name is null"));
        var userId = context.Update.GetFromId();
        var chatId = context.Update.GetChatId();
        if (userId != null && chatId != null)
            if (!await resolver.ContainsCorrelationAsync(userId.Value))
                await resolver.AddCorrelationAsync(userId.Value, chatId.Value);

        await next(context);
    }
}