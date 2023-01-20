using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace GBMSTelegramBotFramework.Extensions;

public static class UpdateContextReplyExtensions
{
    public static Task WithText(this IUpdateContextReply reply, string text,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        bool? disableWebPagePreview = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendTextMessageAsync(GetChatId(reply.Context), text, parseMode, entities,
            disableWebPagePreview,
            disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup,
            cancellationToken);

    public static Task DeleteMessage(this IUpdateContextReply reply,
        int messageId,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.DeleteMessageAsync(GetChatId(reply.Context), messageId, cancellationToken);

    public static Task CopyMessage(this IUpdateContextReply reply,
        ChatId fromChatId,
        int messageId,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.CopyMessageAsync(GetChatId(reply.Context), fromChatId, messageId, caption,
            parseMode,
            captionEntities, disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply,
            replyMarkup, cancellationToken);

    public static Task ForwardMessage(this IUpdateContextReply reply,
        ChatId fromChatId,
        int messageId,
        bool? disableNotification = default,
        bool? protectContent = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.ForwardMessageAsync(GetChatId(reply.Context), fromChatId, messageId,
            disableNotification,
            protectContent, cancellationToken);

    public static Task WithDice(this IUpdateContextReply reply,
        Emoji? emoji = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendDiceAsync(GetChatId(reply.Context), emoji, disableNotification,
            protectContent,
            replyToMessageId, allowSendingWithoutReply, replyMarkup, cancellationToken);

    public static Task WithContact(this IUpdateContextReply reply,
        string phoneNumber,
        string firstName,
        string? lastName = default,
        string? vCard = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendContactAsync(GetChatId(reply.Context), phoneNumber, firstName, lastName,
            vCard,
            disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup,
            cancellationToken);

    public static Task WithLocation(this IUpdateContextReply reply,
        double latitude,
        double longitude,
        int? livePeriod = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendLocationAsync(GetChatId(reply.Context), latitude, longitude, livePeriod,
            heading,
            proximityAlertRadius, disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply,
            replyMarkup, cancellationToken);

    public static Task WithVenue(this IUpdateContextReply reply,
        double latitude,
        double longitude,
        string title,
        string address,
        string? foursquareId = default,
        string? foursquareType = default,
        string? googlePlaceId = default,
        string? googlePlaceType = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendVenueAsync(GetChatId(reply.Context), latitude, longitude, title, address,
            foursquareId, foursquareType, googlePlaceId, googlePlaceType, disableNotification, protectContent,
            replyToMessageId, allowSendingWithoutReply, replyMarkup, cancellationToken);

    public static Task WithAudio(this IUpdateContextReply reply,
        InputOnlineFile audio,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        int? duration = default,
        string? performer = default,
        string? title = default,
        InputMedia? thumb = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendAudioAsync(GetChatId(reply.Context), audio, caption, parseMode,
            captionEntities, duration, performer, title, thumb, disableNotification, protectContent,
            replyToMessageId, allowSendingWithoutReply, replyMarkup, cancellationToken);

    public static Task WithDocument(this IUpdateContextReply reply,
        InputOnlineFile document,
        InputMedia? thumb = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? disableContentTypeDetection = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendDocumentAsync(GetChatId(reply.Context), document, thumb, caption, parseMode,
            captionEntities, disableContentTypeDetection, disableNotification, protectContent, replyToMessageId,
            allowSendingWithoutReply, replyMarkup, cancellationToken);

    public static Task WithPhoto(this IUpdateContextReply reply,
        InputOnlineFile photo,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendPhotoAsync(GetChatId(reply.Context), photo, caption, parseMode,
            captionEntities, disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply,
            replyMarkup, cancellationToken);

    public static Task WithGame(this IUpdateContextReply reply,
        string gameShortName,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendGameAsync(GetChatId(reply.Context), gameShortName, disableNotification,
            protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup, cancellationToken);

    public static Task WithSticker(this IUpdateContextReply reply,
        InputOnlineFile sticker,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendStickerAsync(GetChatId(reply.Context), sticker, disableNotification,
            protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup, cancellationToken);

    public static Task WithVideo(this IUpdateContextReply reply, InputOnlineFile video,
        int? duration = default,
        int? width = default,
        int? height = default,
        InputMedia? thumb = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? supportsStreaming = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendVideoAsync(GetChatId(reply.Context), video, duration, width, height, thumb,
            caption, parseMode, captionEntities, supportsStreaming, disableNotification, protectContent,
            replyToMessageId, allowSendingWithoutReply, replyMarkup, cancellationToken);

    public static Task WithVideoNote(this IUpdateContextReply reply,
        InputTelegramFile videoNote,
        int? duration = default,
        int? length = default,
        InputMedia? thumb = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendVideoNoteAsync(GetChatId(reply.Context), videoNote, duration, length, thumb,
            disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup,
            cancellationToken);

    public static Task WithVoice(this IUpdateContextReply reply,
        InputOnlineFile voice,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        int? duration = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendVoiceAsync(GetChatId(reply.Context), voice, caption, parseMode,
            captionEntities, duration, disableNotification, protectContent, replyToMessageId,
            allowSendingWithoutReply, replyMarkup, cancellationToken);

    public static Task WithAnimation(this IUpdateContextReply reply,
        InputOnlineFile animation,
        int? duration = default,
        int? width = default,
        int? height = default,
        InputMedia? thumb = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        int? replyToMessageId = default,
        bool? allowSendingWithoutReply = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default) =>
        reply.Context.BotContext.Client.SendAnimationAsync(GetChatId(reply.Context), animation, duration, width, height,
            thumb,
            caption, parseMode, captionEntities, disableNotification, protectContent, replyToMessageId,
            allowSendingWithoutReply, replyMarkup, cancellationToken);

    private static long GetChatId(UpdateContext updateContext) => updateContext.Update.GetChatId() ??
                                                                  throw new InvalidOperationException(
                                                                      "Cannot get ChatId from Update");
}