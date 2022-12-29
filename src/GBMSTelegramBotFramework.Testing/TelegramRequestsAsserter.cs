using FluentAssertions;
using Telegram.Bot.Requests.Abstractions;

namespace GBMSTelegramBotFramework.Testing;

public class TelegramRequestsAsserter : ITelegramRequestsAsserter
{
    private readonly IList<IRequest> _requests;
    private int _currentRequest;

    public TelegramRequestsAsserter(IEnumerable<IRequest> requests)
    {
        _requests = requests.ToList();
    }

    public ITelegramRequestsAsserter AssertRequest(Action<IRequest> assertAction)
    {
        _currentRequest.Should().BeLessThan(_requests.Count);
        assertAction(_requests[_currentRequest]);
        return this;
    }

    public ITelegramRequestsAsserter GoToNextRequest()
    {
        _currentRequest++;
        return this;
    }
}