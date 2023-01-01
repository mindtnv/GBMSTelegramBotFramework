using GBMSTelegramBotFramework.Abstractions;
using Telegram.Bot.Types.Enums;

namespace GBMSTelegramBotFramework;

public class BotOptionsConfigurator : IBotOptionsConfigurator
{
    private readonly BotOptions _botOptions;

    public BotOptionsConfigurator(BotOptions botOptions)
    {
        _botOptions = botOptions;
    }

    public IBotOptionsConfigurator WithName(string name)
    {
        _botOptions.Name = name;
        return this;
    }

    public IBotOptionsConfigurator WithToken(string token)
    {
        _botOptions.Token = token;
        return this;
    }

    public IBotOptionsConfigurator WithUpdateTypes(params UpdateType[] updateTypes)
    {
        _botOptions.UpdateTypes = updateTypes;
        return this;
    }
}