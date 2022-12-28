using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Testing;

public class MessageBuilder
{
    private readonly Message _message = new();

    public MessageBuilder()
    {
        _message.Date = DateTime.Now;
    }

    public MessageBuilder WithText(string text)
    {
        _message.Text = text;
        return this;
    }

    public MessageBuilder From(User user)
    {
        _message.From = user;
        return this;
    }

    public MessageBuilder WithChat(Chat chat)
    {
        _message.Chat = chat;
        return this;
    }

    public MessageBuilder WithDate(DateTime dateTime)
    {
        _message.Date = dateTime;
        return this;
    }

    public MessageBuilder WithId(int id)
    {
        _message.MessageId = id;
        return this;
    }

    public Message Build() => _message;
}