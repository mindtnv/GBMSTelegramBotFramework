using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace GBMSTelegramBotFramework;

public class BotRegistrationConfigurator : IBotRegistrationConfigurator
{
    private readonly BotOptions _botOptions = new();
    private readonly BotOptionsConfigurator _optionsConfigurator;
    private readonly UpdatePipelineConfigurator _pipelineConfigurator;
    private ITelegramBotClient? _client;

    public BotRegistrationConfigurator(IServiceCollection services)
    {
        Services = services;
        _pipelineConfigurator = new UpdatePipelineConfigurator(services);
        _optionsConfigurator = new BotOptionsConfigurator(_botOptions);
    }

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

    public void Configure()
    {
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
            _pipelineConfigurator.ConfigureBuilder(updatePipelineBuilder);
            bot.UpdateHandler = updatePipelineBuilder.Build();

            return bot;
        });
    }

    private ITelegramBotClient? CreateClientFromOptions(BotOptions options) =>
        options.Token is null ? null : new TelegramBotClient(options.Token);
}