using FluentAssertions;
using Telegram.Bot.Requests;

namespace GBMSTelegramBotFramework.Testing.Extensions;

public static class TelegramRequestsAsserterExtensions
{
    public static ITelegramRequestsAsserter ShouldSendMessageWithText(this ITelegramRequestsAsserter asserter,
        string text)
    {
        asserter.AssertRequest(r =>
        {
            r.Should().BeOfType<SendMessageRequest>();
            r.As<SendMessageRequest>().Text.Should().Be(text);
        });
        return asserter;
    }

    public static ITelegramRequestsAsserter ShouldSendMessageWithText(this ITelegramRequestsAsserter asserter,
        string text, int chatId)
    {
        asserter.ShouldSendMessageWithText(text)
                .GoToNextRequest()
                .AssertRequest(r => r.As<SendMessageRequest>().ChatId.Should().Be(chatId));
        return asserter;
    }
}