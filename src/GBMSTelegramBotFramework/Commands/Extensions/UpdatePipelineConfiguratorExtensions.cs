using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Commands.Extensions;

public static class UpdatePipelineConfiguratorExtensions
{
    public static IUpdatePipelineConfigurator WithCommand<TCommand>(this IUpdatePipelineConfigurator configurator)
        where TCommand : CommandBase<TCommand>
    {
        if (!configurator.Features.Contains<ICommandDescriptorProvider>())
            configurator.Features.Set<ICommandDescriptorProvider>(new CommandDescriptorProvider());

        var provider = configurator.Features.Get<ICommandDescriptorProvider>()!;
        var ctor = typeof(TCommand).GetConstructors().First();
        var parameters = ctor.GetParameters();
        var descriptor =
            ctor.Invoke(parameters.Select(x => Convert.ChangeType(null, x.ParameterType)).ToArray()) as
                ICommandDescriptor;
        provider.AddCommandDescriptor(descriptor!);
        configurator.Services.AddScoped<TCommand>();
        return configurator;
    }

    public static IUpdatePipelineConfigurator WithCommand<TCommandDescriptor>(
        this IUpdatePipelineConfigurator configurator, TCommandDescriptor descriptor)
        where TCommandDescriptor : ICommandDescriptor
    {
        if (!configurator.Features.Contains<ICommandDescriptorProvider>())
            configurator.Features.Set<ICommandDescriptorProvider>(new CommandDescriptorProvider());

        var provider = configurator.Features.Get<ICommandDescriptorProvider>()!;
        provider.AddCommandDescriptor(descriptor);
        if (descriptor.CommandType != null)
            configurator.Services.AddScoped(descriptor.CommandType);

        return configurator;
    }

    public static IUpdatePipelineConfigurator UseCommands(this IUpdatePipelineConfigurator configurator)
    {
        var provider = configurator.Features.Get<ICommandDescriptorProvider>() ?? new CommandDescriptorProvider();
        configurator.UseHandler(new CommandsHandler(provider));
        return configurator;
    }
}