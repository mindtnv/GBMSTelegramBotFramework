using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.EntityFramework.Extensions;

public static class BotRegistrationConfiguratorExtensions
{
    public static IBotRegistrationConfigurator UseEfChatIdResolver<TContext>(
        this IBotRegistrationConfigurator configurator) where TContext : DbBotContext
    {
        configurator.Services.AddSingleton<IChatIdResolverStore, EfChatIdResolverStore>();
        configurator.Services.AddScoped<DbBotContext, TContext>();
        return configurator;
    }
}