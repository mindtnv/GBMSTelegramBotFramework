using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States.Extensions;

public static class BotRegistrationConfiguratorExtensions
{
    public static IBotStateConfigurator WithState(this IBotRegistrationConfigurator configurator,
        Action<IBotStateConfigurator> configure)
    {
        var botStateConfigurator = new BotStateConfigurator(configurator.Services, configurator.BotFeatures);
        configure(botStateConfigurator);
        configurator.Configure((cfg, provider) =>
        {
            var stateDefinition = botStateConfigurator.BuildBotStateDefinition(provider);
            var store = EnsureStateStoreInFeatures(configurator.BotFeatures);
            store.AddState(stateDefinition);
        });
        return botStateConfigurator;
    }

    public static IBotStateConfigurator WithState<T>(this IBotRegistrationConfigurator configurator)
        where T : class, IBotStateDescriptor
    {
        return configurator.WithState(cfg => cfg.WithState<T>());
    }

    private static IBotStateStore EnsureStateStoreInFeatures(IFeaturesCollection collection)
    {
        if (!collection.Contains<IBotStateStore>())
            collection.Set<IBotStateStore>(new BotStateStore());

        return collection.Get<IBotStateStore>()!;
    }
}