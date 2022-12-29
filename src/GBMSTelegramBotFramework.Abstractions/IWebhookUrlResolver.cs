namespace GBMSTelegramBotFramework.Abstractions;

public interface IWebhookUrlResolver
{
    string GetUrl(IBot bot);
    string GetPath(IBot bot);
    string GetTemplate(IBot bot);
}