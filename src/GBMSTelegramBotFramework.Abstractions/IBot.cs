namespace GBMSTelegramBotFramework.Abstractions;

public interface IBot
{
    BotOptions Options { get; }
    UpdateDelegate UpdateHandler { get; }
}