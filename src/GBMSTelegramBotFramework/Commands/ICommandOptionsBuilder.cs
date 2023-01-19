namespace GBMSTelegramBotFramework.Commands;

public interface ICommandOptionsBuilder
{
    ICommandOptionsBuilder WithAliases(IEnumerable<string> aliases);
    CommandOptions Build();
}