using Telegram.Bot;

namespace GBMSTelegramBotFramework.Abstractions;

public interface IBot
{
    IServiceProvider Services { get; }
    BotOptions Options { get; }
    ITelegramBotClient Client { get; }
    IFeaturesCollection Features { get; }
    UpdateDelegate UpdateHandler { get; }
}