using System.Runtime.Serialization;

namespace GBMSTelegramBotFramework.States;

public class BotStateNotFoundException : Exception
{
    public string StateName { get; set; }

    public BotStateNotFoundException(string stateName) : this("State not found: " + stateName, stateName)
    {
    }

    protected BotStateNotFoundException(SerializationInfo info, StreamingContext context, string stateName) : base(info,
        context)
    {
        StateName = stateName;
    }

    public BotStateNotFoundException(string message, string stateName) : base(message)
    {
        StateName = stateName;
    }

    public BotStateNotFoundException(string message, Exception innerException, string stateName) : base(message,
        innerException)
    {
        StateName = stateName;
    }
}