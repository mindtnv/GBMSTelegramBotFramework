using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GBMSTelegramBotFramework.Abstractions.Extensions;

public static class UpdateExtensions
{
    public static long? GetUserId(this Update update)
    {
        return update.Type switch
        {
            UpdateType.Message            => update.Message!.From!.Id,
            UpdateType.CallbackQuery      => update.CallbackQuery!.From.Id,
            UpdateType.EditedMessage      => update.EditedMessage!.From!.Id,
            UpdateType.ChannelPost        => update.ChannelPost!.From!.Id,
            UpdateType.EditedChannelPost  => update.EditedChannelPost!.From!.Id,
            UpdateType.InlineQuery        => update.InlineQuery!.From.Id,
            UpdateType.ChosenInlineResult => update.ChosenInlineResult!.From.Id,
            UpdateType.ShippingQuery      => update.ShippingQuery!.From.Id,
            UpdateType.PreCheckoutQuery   => update.PreCheckoutQuery!.From.Id,
            UpdateType.Unknown            => null,
            UpdateType.Poll               => null,
            UpdateType.PollAnswer         => update.PollAnswer!.User.Id,
            UpdateType.MyChatMember       => update.MyChatMember!.From.Id,
            UpdateType.ChatMember         => update.ChatMember!.From.Id,
            UpdateType.ChatJoinRequest    => update.ChatJoinRequest!.From.Id,
            _                             => throw new ArgumentOutOfRangeException(),
        };
    }
}