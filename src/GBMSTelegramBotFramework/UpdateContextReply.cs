using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class UpdateContextReply : IUpdateContextReply
{
    public UpdateContextReply(UpdateContext context)
    {
        Context = context;
    }

    public UpdateContext Context { get; }
}