namespace GBMSTelegramBotFramework.Commands;

public class CommandDescriptor : ICommandDescriptor
{
    public Type? CommandType { get; set; }
    public CommandOptions Options { get; set; }
    public ICommand? Instance { get; set; }
}