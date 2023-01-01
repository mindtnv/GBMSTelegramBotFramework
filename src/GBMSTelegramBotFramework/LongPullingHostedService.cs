using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace GBMSTelegramBotFramework;

public class LongPullingHostedService : IHostedService
{
    private readonly IEnumerable<IBot> _bots;

    public LongPullingHostedService(IEnumerable<IBot> bots)
    {
        _bots = bots;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var bot in _bots)
            bot.Client.StartReceiving((_, update, _) => bot.HandleUpdateAsync(update),
                (_, _, _) => Task.CompletedTask, cancellationToken: cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}