﻿using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Testing.Builders;

public class UpdateBuilder : BuilderBase
{
    private int _id;
    private Action<MessageBuilder>? _messageBuilderAction;

    public UpdateBuilder WithMessage(Action<MessageBuilder> messageBuilderAction)
    {
        _messageBuilderAction = messageBuilderAction;
        return this;
    }

    public UpdateBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public Update Build() =>
        new Update
        {
            Message = _messageBuilderAction != null
                ? new MessageBuilder().Configure(_messageBuilderAction).Build()
                : null,
            Id = _id,
        };
}