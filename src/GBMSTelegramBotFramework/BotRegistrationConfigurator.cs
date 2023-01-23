using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace GBMSTelegramBotFramework;

public class BotRegistrationConfigurator : IBotRegistrationConfigurator
{
    private readonly BotOptions _botOptions = new();
    private readonly List<Action<IBotRegistrationConfigurator>> _configurators = new();
    private readonly List<Action<IBotRegistrationConfigurator, IServiceProvider>> _configuratorsWithServiceProvider =
        new();
    private readonly BotOptionsConfigurator _optionsConfigurator;
    private readonly UpdatePipelineConfigurator _pipelineConfigurator;
    private ITelegramBotClient? _client;

    public BotRegistrationConfigurator(IServiceCollection services)
    {
        Services = services;
        _pipelineConfigurator = new UpdatePipelineConfigurator(services, BotFeatures);
        On = _pipelineConfigurator.On;
        _optionsConfigurator = new BotOptionsConfigurator(_botOptions);
        BotFeatures.Set<IUpdateContextFeaturesCollectionStore>(new UpdateContextFeaturesCollectionStore());
    }

    public IUpdatePipelineOnConfigurator On { get; }
    public IFeaturesCollection Features => _pipelineConfigurator.Features;
    public IFeaturesCollection BotFeatures { get; } = new FeaturesCollection();
    public IServiceCollection Services { get; }

    public IBotRegistrationConfigurator UseTelegramBotClient(ITelegramBotClient telegramBotClient)
    {
        _client = telegramBotClient;
        return this;
    }

    public IBotRegistrationConfigurator ConfigureUpdatePipeline(Action<IUpdatePipelineConfigurator> configure)
    {
        configure(_pipelineConfigurator);
        return this;
    }

    public IBotRegistrationConfigurator ConfigureOptions(Action<IBotOptionsConfigurator> configure)
    {
        configure(_optionsConfigurator);
        return this;
    }

    public IBotRegistrationConfigurator Configure(Action<IBotRegistrationConfigurator> configure)
    {
        _configurators.Add(configure);
        return this;
    }

    public IBotRegistrationConfigurator Configure(Action<IBotRegistrationConfigurator, IServiceProvider> configure)
    {
        _configuratorsWithServiceProvider.Add(configure);
        return this;
    }

    public void Register()
    {
        _configurators.ForEach(c => c(this));
        Services.AddSingleton<IBot>(provider =>
        {
            _configuratorsWithServiceProvider.ForEach(c => c(this, provider));
            var bot = new Bot(provider)
            {
                Client = _client ?? CreateClientFromOptions(_botOptions) ??
                    throw new InvalidOperationException("TelegramClient is not configured"),
                Options = _botOptions,
                Features = BotFeatures,
            };

            // Configure UpdatePipeline
            var updatePipelineBuilder = new UpdatePipelineBuilder(provider);
            _pipelineConfigurator.ConfigureBuilder(updatePipelineBuilder);
            bot.UpdateHandler = updatePipelineBuilder.Build();

            return bot;
        });
    }

    IUpdatePipelineConfigurator IUpdatePipelineConfigurator.Configure(Action<IUpdatePipelineBuilder> configure) =>
        _pipelineConfigurator.Configure(configure);

    void IUpdatePipelineConfigurator.ConfigureBuilder(IUpdatePipelineBuilder builder)
    {
        _pipelineConfigurator.ConfigureBuilder(builder);
    }

    private ITelegramBotClient? CreateClientFromOptions(BotOptions options) =>
        options.Token is null ? null : new TelegramBotClient(options.Token);
}