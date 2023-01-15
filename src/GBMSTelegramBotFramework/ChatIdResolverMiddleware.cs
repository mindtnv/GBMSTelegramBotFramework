using System.Diagnostics;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;

namespace GBMSTelegramBotFramework;

public class ChatIdResolverMiddleware : IUpdateMiddleware
{
    private readonly IChatIdResolverStore _resolverStore;

    public ChatIdResolverMiddleware(IChatIdResolverStore resolverStore)
    {
        _resolverStore = resolverStore;
    }

    public async Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
    {
        var resolver =
            _resolverStore.GetResolver(context.BotContext.Options.Name ??
                                       throw new InvalidCastException("Bot name is null"));
        var userId = context.Update.GetFromId();
        var chatId = context.Update.GetChatId();
        if (userId != null && chatId != null)
            if (!await resolver.ContainsCorrelationAsync(userId.Value))
            {
                Debug.WriteLine("Correlation [userId: {0}, chatId: {1}] not found. Save it to resolver..", userId,
                    chatId);
                await resolver.AddCorrelationAsync(userId.Value, chatId.Value);
            }

        await next(context);
    }
}