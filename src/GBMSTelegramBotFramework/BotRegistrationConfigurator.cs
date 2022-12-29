using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace GBMSTelegramBotFramework;

public class BotRegistrationConfigurator : IBotRegistrationConfigurator
{
    private readonly UpdatePipelineConfigurator _pipelineConfigurator;
    private ITelegramBotClient? _client;

    public BotRegistrationConfigurator(IServiceCollection services)
    {
        Services = services;
        _pipelineConfigurator = new UpdatePipelineConfigurator(services);
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

    public void Configure()
    {
        Services.AddSingleton<IBot>(provider =>
        {
            var bot = new Bot(provider)
            {
                Client = _client ?? throw new InvalidOperationException("Client is not configured"),
            };

            // Configure UpdatePipeline
            var updatePipelineBuilder = new UpdatePipelineBuilder(provider);
            _pipelineConfigurator.ConfigureBuilder(updatePipelineBuilder);
            bot.UpdateHandler = updatePipelineBuilder.Build();

            return bot;
        });
    }
}