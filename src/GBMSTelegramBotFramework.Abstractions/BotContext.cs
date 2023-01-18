using Telegram.Bot;

namespace GBMSTelegramBotFramework.Abstractions;

public class BotContext
{
    public ITelegramBotClient Client { get; set; }
    public BotOptions Options { get; set; }
    public IFeaturesCollection Features { get; set; }
}