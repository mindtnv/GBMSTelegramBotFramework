using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.EntityFramework.Extensions;

public static class BotRegistrationConfiguratorExtensions
{
    public static IBotRegistrationConfigurator UseEfChatIdResolver<TContext>(
        this IBotRegistrationConfigurator configurator) where TContext : DbBotContext
    {
        configurator.Services.AddTransient<IChatIdResolverStore, EfChatIdResolverStore>();
        configurator.Services.AddTransient<DbBotContext, TContext>();
        return configurator;
    }
}