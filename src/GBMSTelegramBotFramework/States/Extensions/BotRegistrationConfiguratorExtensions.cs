using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.States.Extensions;

public static class BotRegistrationConfiguratorExtensions
{
    public static IBotRegistrationConfigurator WithState(this IBotRegistrationConfigurator configurator,
        Action<IBotStateConfigurator> configure)
    {
        var botStateConfigurator = new BotStateConfigurator(configurator.Services);
        configure(botStateConfigurator);
        configurator.Configure((cfg, provider) =>
        {
            var stateDefinition = botStateConfigurator.BuildBotStateDefinition(provider);
            cfg.ConfigureFeatures(features =>
            {
                var store = EnsureStateStoreInFeatures(features);
                store.AddStateDefinition(stateDefinition);
            });
        });
        return configurator;
    }

    public static IBotRegistrationConfigurator WithState<T>(this IBotRegistrationConfigurator configurator)
        where T : class, IBotState
    {
        configurator.WithState(cfg => cfg.WithState<T>());
        return configurator;
    }

    private static IBotStateStore EnsureStateStoreInFeatures(IFeaturesCollection collection)
    {
        if (!collection.Contains<IBotStateStore>())
            collection.Set<IBotStateStore>(new BotStateStore());

        return collection.Get<IBotStateStore>()!;
    }
}