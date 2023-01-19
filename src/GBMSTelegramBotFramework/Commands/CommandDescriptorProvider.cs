using System.Collections.Concurrent;

namespace GBMSTelegramBotFramework.Commands;

public class CommandDescriptorProvider : ICommandDescriptorProvider
{
    private readonly ConcurrentDictionary<string, ICommandDescriptor> _descriptors = new();
    private readonly List<string> _aliases = new();
    public IEnumerable<string> Aliases => _aliases;

    public void AddCommandDescriptor(ICommandDescriptor commandDescriptor)
    {
        foreach (var alias in commandDescriptor.Options.Aliases)
        {
            _descriptors.TryAdd(alias, commandDescriptor);
            _aliases.Add(alias);
        }
    }

    public ICommandDescriptor? GetCommandDescriptor(string alias) =>
        _descriptors.TryGetValue(alias, out var descriptor) ? descriptor : null;
}