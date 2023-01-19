using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Extensions;

public static class UpdatePipelineOnConfiguratorExtensions
{
    public static IUpdatePipelineConfigurator Sticker(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnSticker += handler);

    public static IUpdatePipelineConfigurator Text(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnText += handler);

    public static IUpdatePipelineConfigurator Text(this IUpdatePipelineOnConfigurator configurator, string text,
        Func<UpdateContext, Task> handler)
    {
        return UseEvent(configurator, h => h.OnText += async context =>
        {
            if (context.Update.Message!.Text == text)
                await handler(context);
        });
    }

    public static IUpdatePipelineConfigurator Photo(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPhoto += handler);

    public static IUpdatePipelineConfigurator Video(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnVideo += handler);

    public static IUpdatePipelineConfigurator Voice(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnVoice += handler);

    public static IUpdatePipelineConfigurator Document(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnDocument += handler);

    public static IUpdatePipelineConfigurator OnAudio(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnAudio += handler);

    public static IUpdatePipelineConfigurator Location(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnLocation += handler);

    public static IUpdatePipelineConfigurator Contact(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnContact += handler);

    public static IUpdatePipelineConfigurator Venue(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnVenue += handler);

    public static IUpdatePipelineConfigurator CallbackQuery(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnCallbackQuery += handler);

    public static IUpdatePipelineConfigurator CallbackQuery(this IUpdatePipelineOnConfigurator configurator,
        string data, Func<UpdateContext, Task> handler)
    {
        return UseEvent(configurator, h => h.OnText += async context =>
        {
            if (context.Update.CallbackQuery!.Data == data)
                await handler(context);
        });
    }

    public static IUpdatePipelineConfigurator InlineQuery(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnInlineQuery += handler);

    public static IUpdatePipelineConfigurator ChosenInlineResult(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnChosenInlineResult += handler);

    public static IUpdatePipelineConfigurator ShippingQuery(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnShippingQuery += handler);

    public static IUpdatePipelineConfigurator PreCheckoutQuery(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPreCheckoutQuery += handler);

    public static IUpdatePipelineConfigurator Poll(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPoll += handler);

    public static IUpdatePipelineConfigurator PollAnswer(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPollAnswer += handler);

    public static IUpdatePipelineConfigurator MyChatMember(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnMyChatMember += handler);

    public static IUpdatePipelineConfigurator ChatMember(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnChatMember += handler);

    public static IUpdatePipelineConfigurator Dice(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnDice += handler);

    public static IUpdatePipelineConfigurator Game(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnGame += handler);

    public static IUpdatePipelineConfigurator Invoice(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnInvoice += handler);

    public static IUpdatePipelineConfigurator SuccessfulPayment(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnSuccessfulPayment += handler);

    public static IUpdatePipelineConfigurator PassportData(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPassportData += handler);

    public static IUpdatePipelineConfigurator EditedMessage(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnEditedMessage += handler);

    public static IUpdatePipelineConfigurator EditedChannelPost(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnEditedChannelPost += handler);

    public static IUpdatePipelineConfigurator ChannelPost(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnChannelPost += handler);

    public static IUpdatePipelineConfigurator ChatJoinRequest(this IUpdatePipelineOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnChatJoinRequest += handler);

    private static IUpdatePipelineConfigurator UseEvent(IUpdatePipelineOnConfigurator configurator,
        Action<EventBasedUpdateHandler> subscribeAction)
    {
        var h = new EventBasedUpdateHandler();
        subscribeAction(h);
        configurator.Configurator.UseHandler(h);
        return configurator.Configurator;
    }
}