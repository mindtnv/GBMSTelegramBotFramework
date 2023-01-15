namespace GBMSTelegramBotFramework.EntityFramework.Models;

public class UserIdCorrelation
{
    public int Id { get; set; }
    public string BotName { get; set; }
    public long UserId { get; set; }
    public long ChatId { get; set; }
}