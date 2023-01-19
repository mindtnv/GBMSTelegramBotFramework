namespace GBMSTelegramBotFramework.Commands;

public interface ICommandDescriptor
{
    Type? CommandType { get; }
    CommandOptions Options { get; }
    ICommand? Instance { get; }
}