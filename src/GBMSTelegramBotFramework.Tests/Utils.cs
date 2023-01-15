using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Tests;

public static class Utils
{
    public static IBot CreateTestBot(Action<IBotRegistrationConfigurator> configure)
    {
        var services = new ServiceCollection();
        services.AddTelegramBot(bot =>
        {
            bot.ConfigureOptions(x => { x.WithName("testing-bot"); });
            bot.UseTestingClient();
            bot.Configure(configure);
        });
        var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
        });
        var bot = provider.GetRequiredService<IBot>();
        return bot;
    }
}