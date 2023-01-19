namespace GBMSTelegramBotFramework.Commands;

public interface ICommandDescriptorProvider
{
    IEnumerable<string> Aliases { get; }
    void AddCommandDescriptor(ICommandDescriptor commandDescriptor);
    ICommandDescriptor? GetCommandDescriptor(string alias);
}