using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Testing;

public class UpdateBuilder
{
    private readonly Update _update = new();

    public UpdateBuilder WithId(int id)
    {
        _update.Id = id;
        return this;
    }

    public UpdateBuilder WithMessage(Action<MessageBuilder> configure)
    {
        var builder = new MessageBuilder();
        configure(builder);
        _update.Message = builder.Build();
        return this;
    }

    public Update Build() => _update;
}