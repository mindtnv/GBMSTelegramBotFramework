namespace GBMSTelegramBotFramework.Abstractions;

public interface IBotProvider
{
    IBot GetBot(string botName);
}