using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.Options;

namespace GBMSTelegramBotFramework;

public class WebhookUrlResolver : IWebhookUrlResolver
{
    private readonly WebhookOptions _options;

    public WebhookUrlResolver(IOptions<WebhookOptions> options)
    {
        _options = options.Value;
    }

    public string GetUrl(IBot bot) => $"{_options.Url}{GetPath(bot)}";

    public string GetPath(IBot bot) =>
        GetTemplate(bot).Replace("{name}", bot.Options.Name).Replace("{secret}", _options.Secret);

    public string GetTemplate(IBot bot) => "/bots/{name}/{secret}/";
}