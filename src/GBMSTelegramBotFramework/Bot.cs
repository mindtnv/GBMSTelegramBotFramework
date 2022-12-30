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
    public BotOptions Options { get; set; } = null!;
    public ITelegramBotClient Client { get; set; } = null!;
    public UpdateDelegate UpdateHandler { get; set; } = null!;
}