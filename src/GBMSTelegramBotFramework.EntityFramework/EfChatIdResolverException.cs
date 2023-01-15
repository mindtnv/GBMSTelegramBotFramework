namespace GBMSTelegramBotFramework.EntityFramework;

public class EfChatIdResolverException : Exception
{
    public long UserId { get; set; }
    public string Message { get; set; }

    public EfChatIdResolverException(string message, long userId)
    {
        UserId = userId;
        Message = message;
    }
}