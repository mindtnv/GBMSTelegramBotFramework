using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Commands.Extensions;

public static class UpdatePipelineOnConfiguratorExtensions
{
    public static IUpdatePipelineConfigurator Command(this IUpdatePipelineOnConfigurator configurator,
        IEnumerable<string> aliases, Func<UpdateContext, string[], Task> handler)
    {
        var options = new CommandOptionsBuilder().WithAliases(aliases).Build();
        var command = new FuncCommand(handler);
        var descriptor = new CommandDescriptor
        {
            Options = options,
            Instance = command,
        };
        configurator.Configurator.WithCommand(descriptor);
        return configurator.Configurator;
    }

    public static IUpdatePipelineConfigurator Command(this IUpdatePipelineOnConfigurator configurator, string alias,
        Func<UpdateContext, string[], Task> handler) => configurator.Command(new[] {alias}, handler);
}