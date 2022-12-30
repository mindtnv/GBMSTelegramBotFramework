using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Tests;

public static class Utils
{
    public static IBot CreateTestBotWithPipeline(Action<IUpdatePipelineConfigurator> configure)
    {
        var services = new ServiceCollection();
        services.AddTelegramBot(bot =>
        {
            bot.UseTestingClient();
            bot.ConfigureUpdatePipeline(configure);
        });
        var provider = services.BuildServiceProvider();
        var bot = provider.GetRequiredService<IBot>();
        return bot;
    }
}