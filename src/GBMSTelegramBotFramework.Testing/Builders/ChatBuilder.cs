using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Testing.Builders;

public class ChatBuilder : BuilderBase
{
    private long _id;

    public ChatBuilder WithId(long id)
    {
        _id = id;
        return this;
    }

    public Chat Build() =>
        new()
        {
            Id = _id,
        };
}