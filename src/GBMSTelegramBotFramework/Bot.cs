using GBMSTelegramBotFramework.Abstractions;
using Telegram.Bot;

namespace GBMSTelegramBotFramework;

internal class Bot : IBot
{
    public Bot(IServiceProvider services)
    {
        Services = services;
    }

    public IServiceProvider Services { get; }
    public BotOptions Options { get; set; }
    public ITelegramBotClient Client { get; set; }
    public UpdateDelegate UpdateHandler { get; set; }
}