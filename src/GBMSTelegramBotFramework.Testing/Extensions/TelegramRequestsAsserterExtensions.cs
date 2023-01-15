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
        string text, long chatId)
    {
        asserter.ShouldSendMessageWithText(text)
                .AssertRequest(r => r.As<SendMessageRequest>().ChatId.Identifier.Should().Be(chatId));
        return asserter;
    }

    public static ITelegramRequestsAsserter ShouldNotSendMessage(this ITelegramRequestsAsserter asserter)
    {
        asserter.IsFinished.Should().BeTrue();
        return asserter;
    }
}