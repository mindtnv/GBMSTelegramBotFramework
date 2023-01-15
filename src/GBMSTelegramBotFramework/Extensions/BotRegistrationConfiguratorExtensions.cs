using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Extensions;

public static class BotRegistrationConfiguratorExtensions
{
    public static IBotRegistrationConfigurator UseHandler<THandler>(this IBotRegistrationConfigurator configurator)
        where THandler : IUpdateHandler
    {
        configurator.ConfigureUpdatePipeline(p => p.UseHandler<THandler>());
        return configurator;
    }

    public static IBotRegistrationConfigurator UseCommand<TCommand>(this IBotRegistrationConfigurator configurator)
        where TCommand : CommandHandlerBase
    {
        configurator.ConfigureUpdatePipeline(p => p.UseCommand<TCommand>());
        return configurator;
    }

    public static IBotRegistrationConfigurator UseCommandNotFoundHandler(this IBotRegistrationConfigurator configurator,
        string message)
    {
        configurator.ConfigureUpdatePipeline(p => p.UseCommandNotFoundHandler(message));
        return configurator;
    }

    public static IBotRegistrationConfigurator UseReadMiddleware(this IBotRegistrationConfigurator configurator)
    {
        configurator.ConfigureUpdatePipeline(p => p.UseReadMiddleware());
        return configurator;
    }
}