using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Extensions;

public static class BotOnConfiguratorExtensions
{
    public static IUpdatePipelineConfigurator Sticker(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnSticker += handler);

    public static IUpdatePipelineConfigurator Text(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnText += handler);

    public static IUpdatePipelineConfigurator Photo(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPhoto += handler);

    public static IUpdatePipelineConfigurator Video(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnVideo += handler);

    public static IUpdatePipelineConfigurator Voice(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnVoice += handler);

    public static IUpdatePipelineConfigurator Document(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnDocument += handler);

    public static IUpdatePipelineConfigurator OnAudio(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnAudio += handler);

    public static IUpdatePipelineConfigurator Location(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnLocation += handler);

    public static IUpdatePipelineConfigurator Contact(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnContact += handler);

    public static IUpdatePipelineConfigurator Venue(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnVenue += handler);

    public static IUpdatePipelineConfigurator CallbackQuery(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnCallbackQuery += handler);

    public static IUpdatePipelineConfigurator InlineQuery(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnInlineQuery += handler);

    public static IUpdatePipelineConfigurator ChosenInlineResult(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnChosenInlineResult += handler);

    public static IUpdatePipelineConfigurator ShippingQuery(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnShippingQuery += handler);

    public static IUpdatePipelineConfigurator PreCheckoutQuery(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPreCheckoutQuery += handler);

    public static IUpdatePipelineConfigurator Poll(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPoll += handler);

    public static IUpdatePipelineConfigurator PollAnswer(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPollAnswer += handler);

    public static IUpdatePipelineConfigurator MyChatMember(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnMyChatMember += handler);

    public static IUpdatePipelineConfigurator ChatMember(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnChatMember += handler);

    public static IUpdatePipelineConfigurator Dice(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnDice += handler);

    public static IUpdatePipelineConfigurator Game(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnGame += handler);

    public static IUpdatePipelineConfigurator Invoice(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnInvoice += handler);

    public static IUpdatePipelineConfigurator SuccessfulPayment(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnSuccessfulPayment += handler);

    public static IUpdatePipelineConfigurator PassportData(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnPassportData += handler);

    public static IUpdatePipelineConfigurator EditedMessage(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnEditedMessage += handler);

    public static IUpdatePipelineConfigurator EditedChannelPost(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnEditedChannelPost += handler);

    public static IUpdatePipelineConfigurator ChannelPost(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnChannelPost += handler);

    public static IUpdatePipelineConfigurator ChatJoinRequest(this IBotOnConfigurator configurator,
        Func<UpdateContext, Task> handler) => UseEvent(configurator, h => h.OnChatJoinRequest += handler);

    private static IUpdatePipelineConfigurator UseEvent(IBotOnConfigurator configurator,
        Action<EventBasedUpdateHandler> subscribeAction)
    {
        var h = new EventBasedUpdateHandler();
        subscribeAction(h);
        configurator.Configurator.UseHandler(h);
        return configurator.Configurator;
    }
}