using Telegram.Bot.Requests.Abstractions;

namespace GBMSTelegramBotFramework.Testing;

public interface ITelegramRequestsAsserter
{
    ITelegramRequestsAsserter AssertRequest(Action<IRequest> assertAction);
    ITelegramRequestsAsserter GoToNextRequest();
}