using Telegram.Bot.Types.Enums;

namespace GBMSTelegramBotFramework.Abstractions;

public interface IBotOptionsConfigurator
{
    IBotOptionsConfigurator WithName(string name);
    IBotOptionsConfigurator WithToken(string token);
    IBotOptionsConfigurator WithUpdateTypes(params UpdateType[] updateTypes);
}