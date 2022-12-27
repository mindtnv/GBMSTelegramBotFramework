using Telegram.Bot;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Abstractions;

public class UpdateContext
{
    public ITelegramBotClient Client { get; set; }
    public IBot Bot { get; set; }
    public Update Update { get; set; }
}