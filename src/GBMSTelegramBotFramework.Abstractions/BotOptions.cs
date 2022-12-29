using Telegram.Bot.Types.Enums;

namespace GBMSTelegramBotFramework.Abstractions;

public class BotOptions
{
    public string Token { get; set; }
    public string Name { get; set; }
    public IEnumerable<UpdateType>? UpdateTypes { get; set; }
}