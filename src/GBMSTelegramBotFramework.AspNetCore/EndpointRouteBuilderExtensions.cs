using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.AspNetCore;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapWebhookUpdateController(this IEndpointRouteBuilder builder)
    {
        var bots = builder.ServiceProvider.GetServices<IBot>();
        var urlResolver = builder.ServiceProvider.GetRequiredService<IWebhookUrlResolver>();
        foreach (var bot in bots)
            builder.MapControllerRoute("UpdateWebhook", "/bots/{name}/{secret}", new
            {
                controller = "Update",
                action = "Post",
            });

        builder.MapControllers();
        return builder;
    }
}