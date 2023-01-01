using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Testing.Builders;

public class MessageBuilder : BuilderBase
{
    private Action<ChatBuilder>? _chatBuilderAction;
    private DateTime _date = DateTime.UnixEpoch;
    private int _id;
    private string? _text;
    private Action<UserBuilder>? _userBuilderAction;

    public MessageBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public MessageBuilder From(Action<UserBuilder> configure)
    {
        _userBuilderAction = configure;
        return this;
    }

    public MessageBuilder WithChat(Action<ChatBuilder> configure)
    {
        _chatBuilderAction = configure;
        return this;
    }

    public MessageBuilder WithDate(DateTime dateTime)
    {
        _date = dateTime;
        return this;
    }

    public MessageBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public Message Build() =>
        new()
        {
            Chat = _chatBuilderAction != null
                ? new ChatBuilder().Configure(_chatBuilderAction).Build()
                : new Chat(),
            From = _userBuilderAction != null
                ? new UserBuilder().Configure(_userBuilderAction).Build()
                : new User(),
            Text = _text,
            Date = _date,
            MessageId = _id,
        };
}