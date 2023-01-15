﻿using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace GBMSTelegramBotFramework.Extensions;

public static class UpdateContextSendExtensions
{
    public static Task SendTextMessageAsync(this UpdateContext context, string text,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        bool? disableWebPagePreview = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default)
    {
        var chatId = context.Update.GetChatId() ?? throw new InvalidOperationException("Cannot get ChatId from Update");
        return context.BotContext.Client.SendTextMessageAsync(chatId, text, parseMode, entities, disableWebPagePreview,
            disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup,
            cancellationToken);
    }

    public static async Task SendTextMessageToUserAsync(this UpdateContext context,
        long userId,
        string text,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        bool? disableWebPagePreview = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default)
    {
        await using var scope = context.Services.CreateAsyncScope();
        var resolver = scope.ServiceProvider.GetRequiredService<IChatIdResolverStore>()
                            .GetResolver(context.BotContext.Options.Name!);
        var chatId = await resolver.GetChatIdAsync(userId);
        await context.BotContext.Client.SendTextMessageAsync(chatId, text, parseMode, entities, disableWebPagePreview,
            disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup,
            cancellationToken);
    }
}