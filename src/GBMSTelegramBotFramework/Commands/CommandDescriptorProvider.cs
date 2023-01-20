using System.Collections.Concurrent;

namespace GBMSTelegramBotFramework.Commands;

public class CommandDescriptorProvider : ICommandDescriptorProvider
{
    private readonly List<string> _aliases = new();
    private readonly ConcurrentDictionary<string, ICommandDescriptor> _descriptors = new();
    public IEnumerable<string> Aliases => _aliases;

    public void AddCommandDescriptor(ICommandDescriptor commandDescriptor)
    {
        foreach (var alias in commandDescriptor.Options.Aliases)
        {
            if (_descriptors.ContainsKey(alias))
                _descriptors.TryRemove(alias, out _);

            _descriptors.TryAdd(alias, commandDescriptor);
            if (!_aliases.Contains(alias))
                _aliases.Add(alias);
        }
    }

    public ICommandDescriptor? GetCommandDescriptor(string alias) =>
        _descriptors.TryGetValue(alias, out var descriptor) ? descriptor : null;
}