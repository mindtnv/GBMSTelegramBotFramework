using System.Diagnostics;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework;

public class LongPullingHostedService : IHostedService
{
    private readonly IEnumerable<IBot> _bots;
    private readonly ILogger<LongPullingHostedService> _logger;

    public LongPullingHostedService(IEnumerable<IBot> bots, ILogger<LongPullingHostedService> logger)
    {
        _bots = bots;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var bot in _bots)
        {
            _logger.LogInformation("Starting long pulling for bot {BotName}", bot.Options.Name);

            async void UpdateHandler(ITelegramBotClient telegramBotClient, Update update,
                CancellationToken cancellationToken1)
            {
                await bot.HandleUpdateAsync(update);
            }

            bot.Client.StartReceiving(UpdateHandler,
                (_, e, _) => { Debug.Fail(e.Message); }, cancellationToken: cancellationToken);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}