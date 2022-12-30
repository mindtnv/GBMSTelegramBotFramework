using Telegram.Bot.Requests.Abstractions;

namespace GBMSTelegramBotFramework.Testing;

public interface ITelegramRequestsAsserter
{
    bool IsFinished { get; }
    ITelegramRequestsAsserter AssertRequest(Action<IRequest> assertAction);
    ITelegramRequestsAsserter GoToNextRequest();
}