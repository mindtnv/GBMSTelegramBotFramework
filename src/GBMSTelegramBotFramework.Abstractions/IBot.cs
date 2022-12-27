namespace GBMSTelegramBotFramework.Abstractions;

public interface IBot
{
    BotOptions Options { get; }
    Type UpdateHandlerType { get; }
}

public interface IBot<THandler> : IBot where THandler : IUpdateHandler
{
}