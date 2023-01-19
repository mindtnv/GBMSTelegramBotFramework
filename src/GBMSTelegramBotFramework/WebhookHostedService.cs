using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace GBMSTelegramBotFramework;

public class WebhookHostedService : IHostedService
{
    private readonly IEnumerable<IBot> _bots;
    private readonly ILogger<WebhookHostedService> _logger;
    private readonly IWebhookUrlResolver _urlResolver;

    public WebhookHostedService(IEnumerable<IBot> bots, ILogger<WebhookHostedService> logger,
        IWebhookUrlResolver urlResolver)
    {
        _bots = bots;
        _logger = logger;
        _urlResolver = urlResolver;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var bot in _bots)
        {
            var url = _urlResolver.GetUrl(bot);
            _logger.LogInformation("Register webhook for bot {BotName} with url {Url}", bot.Options.Name, url);
            await bot.Client.SetWebhookAsync(url, cancellationToken: cancellationToken,
                allowedUpdates: bot.Options.UpdateTypes);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var bot in _bots)
        {
            _logger.LogInformation("Delete webhook for bot {BotName}", bot.Options.Name);
            await bot.Client.DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}