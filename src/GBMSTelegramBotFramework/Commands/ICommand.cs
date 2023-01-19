using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Commands;

public interface ICommand
{
    Task ExecuteAsync(UpdateContext context, string[] args);
}