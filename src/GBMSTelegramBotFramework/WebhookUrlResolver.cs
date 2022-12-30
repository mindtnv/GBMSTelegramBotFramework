using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.Options;

namespace GBMSTelegramBotFramework;

public class WebhookUrlResolver : IWebhookUrlResolver
{
    private readonly string _secret;
    private readonly string _url;

    public WebhookUrlResolver(IOptions<WebhookOptions> options)
    {
        _url = options.Value.Url ?? throw new InvalidOperationException("Webhook url is not set");
        _secret = string.IsNullOrEmpty(options.Value.Secret) ? Guid.NewGuid().ToString() : options.Value.Secret;
    }

    public string GetUrl(IBot bot) => new Uri(new Uri(_url), GetPath(bot)).ToString();

    public string GetPath(IBot bot) =>
        GetTemplate(bot).Replace("{name}", bot.Options.Name).Replace("{secret}", _secret);

    public string GetTemplate(IBot bot) => "/bots/{name}/{secret}/";
}