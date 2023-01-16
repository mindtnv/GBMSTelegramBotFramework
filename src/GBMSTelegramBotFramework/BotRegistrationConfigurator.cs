using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace GBMSTelegramBotFramework;

public class BotRegistrationConfigurator : IBotRegistrationConfigurator
{
    private readonly BotOptions _botOptions = new();
    private readonly List<Action<IBotRegistrationConfigurator>> _configurators = new();
    private readonly BotOptionsConfigurator _optionsConfigurator;
    private readonly UpdatePipelineConfigurator _pipelineConfigurator;
    private ITelegramBotClient? _client;

    public BotRegistrationConfigurator(IServiceCollection services)
    {
        Services = services;
        On = new DefaultBotOnConfigurator(this);
        _pipelineConfigurator = new UpdatePipelineConfigurator(services);
        _optionsConfigurator = new BotOptionsConfigurator(_botOptions);
    }

    public IBotOnConfigurator On { get; }
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

    public void Register()
    {
        foreach (var c in _configurators)
            c(this);

        Services.AddSingleton<IBot>(provider =>
        {
            var bot = new Bot(provider)
            {
                Client = _client ?? CreateClientFromOptions(_botOptions) ??
                    throw new InvalidOperationException("TelegramClient is not configured"),
                Options = _botOptions,
            };

            // Configure UpdatePipeline
            var updatePipelineBuilder = new UpdatePipelineBuilder(provider);
            updatePipelineBuilder.UseMiddleware(typeof(StopMiddleware));
            // updatePipelineBuilder.UseMiddleware(typeof(UserIdResolverMiddleware));
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

    private class DefaultBotOnConfigurator : IBotOnConfigurator
    {
        public DefaultBotOnConfigurator(IBotRegistrationConfigurator configurator)
        {
            Configurator = configurator;
        }

        public IBotRegistrationConfigurator Configurator { get; }
    }
}