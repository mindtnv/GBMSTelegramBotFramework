namespace GBMSTelegramBotFramework.AspNetCore;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseTelegramWebhook(this IApplicationBuilder builder)
    {
        builder.UseRouting();
        builder.UseEndpoints(x => x.MapWebhookUpdateController());
        return builder;
    }
}