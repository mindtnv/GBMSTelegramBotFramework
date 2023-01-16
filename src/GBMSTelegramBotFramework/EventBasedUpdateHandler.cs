using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class EventBasedUpdateHandler : UpdateHandlerBase
{
    public event Func<UpdateContext, Task>? OnSticker;
    public event Func<UpdateContext, Task>? OnText;
    public event Func<UpdateContext, Task>? OnPhoto;
    public event Func<UpdateContext, Task>? OnVideo;
    public event Func<UpdateContext, Task>? OnVoice;
    public event Func<UpdateContext, Task>? OnDocument;
    public event Func<UpdateContext, Task>? OnAudio;
    public event Func<UpdateContext, Task>? OnLocation;
    public event Func<UpdateContext, Task>? OnContact;
    public event Func<UpdateContext, Task>? OnVenue;
    public event Func<UpdateContext, Task>? OnPoll;
    public event Func<UpdateContext, Task>? OnDice;
    public event Func<UpdateContext, Task>? OnGame;
    public event Func<UpdateContext, Task>? OnInvoice;
    public event Func<UpdateContext, Task>? OnSuccessfulPayment;
    public event Func<UpdateContext, Task>? OnPassportData;
    public event Func<UpdateContext, Task>? OnInlineQuery;
    public event Func<UpdateContext, Task>? OnChosenInlineResult;
    public event Func<UpdateContext, Task>? OnCallbackQuery;
    public event Func<UpdateContext, Task>? OnShippingQuery;
    public event Func<UpdateContext, Task>? OnPreCheckoutQuery;
    public event Func<UpdateContext, Task>? OnPollAnswer;
    public event Func<UpdateContext, Task>? OnMyChatMember;
    public event Func<UpdateContext, Task>? OnChatMember;
    public event Func<UpdateContext, Task>? OnEditedMessage;
    public event Func<UpdateContext, Task>? OnChannelPost;
    public event Func<UpdateContext, Task>? OnEditedChannelPost;
    public event Func<UpdateContext, Task>? OnChatJoinRequest;

    public override Task OnInlineQueryAsync(UpdateContext context) =>
        OnInlineQuery?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnChannelPostAsync(UpdateContext context) =>
        OnChannelPost?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnChatJoinRequestAsync(UpdateContext context) =>
        OnChatJoinRequest?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnEditedChannelPostAsync(UpdateContext context) =>
        OnEditedChannelPost?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnEditedMessageAsync(UpdateContext context) =>
        OnEditedMessage?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnChosenInlineResultAsync(UpdateContext context) =>
        OnChosenInlineResult?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnCallbackQueryAsync(UpdateContext context) =>
        OnCallbackQuery?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnShippingQueryAsync(UpdateContext context) =>
        OnShippingQuery?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnPreCheckoutQueryAsync(UpdateContext context) =>
        OnPreCheckoutQuery?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnPollAnswerAsync(UpdateContext context) =>
        OnPollAnswer?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnMyChatMemberAsync(UpdateContext context) =>
        OnMyChatMember?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnChatMemberAsync(UpdateContext context) =>
        OnChatMember?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnPollAsync(UpdateContext context) =>
        OnPoll?.Invoke(context) ?? Task.CompletedTask;

    public override Task OnMessageAsync(UpdateContext context)
    {
        var message = context.Update.Message!;
        if (message.Sticker is not null)
            return OnSticker?.Invoke(context) ?? Task.CompletedTask;
        if (message.Text is not null)
            return OnText?.Invoke(context) ?? Task.CompletedTask;
        if (message.Photo is not null)
            return OnPhoto?.Invoke(context) ?? Task.CompletedTask;
        if (message.Video is not null)
            return OnVideo?.Invoke(context) ?? Task.CompletedTask;
        if (message.Voice is not null)
            return OnVoice?.Invoke(context) ?? Task.CompletedTask;
        if (message.Document is not null)
            return OnDocument?.Invoke(context) ?? Task.CompletedTask;
        if (message.Audio is not null)
            return OnAudio?.Invoke(context) ?? Task.CompletedTask;
        if (message.Contact is not null)
            return OnContact?.Invoke(context) ?? Task.CompletedTask;
        if (message.Dice is not null)
            return OnDice?.Invoke(context) ?? Task.CompletedTask;
        if (message.Game is not null)
            return OnGame?.Invoke(context) ?? Task.CompletedTask;
        if (message.Location is not null)
            return OnLocation?.Invoke(context) ?? Task.CompletedTask;
        if (message.Venue is not null)
            return OnVenue?.Invoke(context) ?? Task.CompletedTask;
        if (message.Invoice is not null)
            return OnInvoice?.Invoke(context) ?? Task.CompletedTask;
        if (message.SuccessfulPayment is not null)
            return OnSuccessfulPayment?.Invoke(context) ?? Task.CompletedTask;
        if (message.PassportData is not null)
            return OnPassportData?.Invoke(context) ?? Task.CompletedTask;

        return Task.CompletedTask;
    }
}