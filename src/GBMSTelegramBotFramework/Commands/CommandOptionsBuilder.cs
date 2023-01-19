namespace GBMSTelegramBotFramework.Commands;

public class CommandOptionsBuilder : ICommandOptionsBuilder
{
    private readonly List<string> _aliases = new();

    public ICommandOptionsBuilder WithAliases(IEnumerable<string> aliases)
    {
        _aliases.AddRange(aliases);
        return this;
    }

    public CommandOptions Build() =>
        new CommandOptions
        {
            Aliases = _aliases,
        };
}