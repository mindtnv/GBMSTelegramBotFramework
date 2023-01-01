namespace GBMSTelegramBotFramework.Abstractions.Extensions;

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
}