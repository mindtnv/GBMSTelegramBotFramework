using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Abstractions;

public interface IBotRegistrationConfigurator
{
    IServiceCollection Services { get; }
    IBotRegistrationConfigurator UseTelegramBotClient(ITelegramBotClient telegramBotClient);
    IBotRegistrationConfigurator ConfigureUpdatePipeline(Action<IUpdatePipelineConfigurator> configure);
    IBotRegistrationConfigurator ConfigureOptions(Action<IBotOptionsConfigurator> configure);
    void Configure();
}