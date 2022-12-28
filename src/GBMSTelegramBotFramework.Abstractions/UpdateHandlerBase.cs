using Telegram.Bot.Types.Enums;

namespace GBMSTelegramBotFramework.Abstractions;

public abstract class UpdateHandlerBase : IUpdateHandler
{
    public Task HandleUpdateAsync(UpdateContext context) => context.Update.Type switch
    {
        UpdateType.Unknown            => throw new InvalidOperationException(),
        UpdateType.Message            => OnMessageAsync(context),
        UpdateType.InlineQuery        => OnInlineQueryAsync(context),
        UpdateType.ChosenInlineResult => OnChosenInlineResultAsync(context),
        UpdateType.CallbackQuery      => OnCallbackQueryAsync(context),
        UpdateType.EditedMessage      => OnEditedMessageAsync(context),
        UpdateType.ChannelPost        => OnChannelPostAsync(context),
        UpdateType.EditedChannelPost  => OnEditedChannelPostAsync(context),
        UpdateType.ShippingQuery      => OnShippingQueryAsync(context),
        UpdateType.PreCheckoutQuery   => OnPreCheckoutQueryAsync(context),
        UpdateType.Poll               => OnPollAsync(context),
        UpdateType.PollAnswer         => OnPollAnswerAsync(context),
        UpdateType.MyChatMember       => OnMyChatMemberAsync(context),
        UpdateType.ChatMember         => OnChatMemberAsync(context),
        UpdateType.ChatJoinRequest    => OnChatJoinRequestAsync(context),
        _                             => throw new ArgumentOutOfRangeException()
    };

    public virtual Task OnMessageAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnInlineQueryAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnChosenInlineResultAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnCallbackQueryAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnEditedMessageAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnChannelPostAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnEditedChannelPostAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnShippingQueryAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnPreCheckoutQueryAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnPollAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnPollAnswerAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnMyChatMemberAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnChatMemberAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnChatJoinRequestAsync(UpdateContext context) => Task.CompletedTask;
    public virtual Task OnErrorAsync(UpdateContext context, Exception exception) => Task.CompletedTask;
}