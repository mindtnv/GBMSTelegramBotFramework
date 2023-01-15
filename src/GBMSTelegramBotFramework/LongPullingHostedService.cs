using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

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
            bot.Client.StartReceiving(async (_, update, _) => { await bot.HandleUpdateAsync(update); },
                (_, _, _) => Task.CompletedTask, cancellationToken: cancellationToken);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}