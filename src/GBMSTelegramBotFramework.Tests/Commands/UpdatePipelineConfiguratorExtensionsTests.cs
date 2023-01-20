using FluentAssertions;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Commands;
using GBMSTelegramBotFramework.Commands.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Tests.Commands;

[TestFixture(Category = "Commands")]
public class UpdatePipelineConfiguratorExtensionsTests
{
    [SetUp]
    public void SetUp()
    {
        _services = new ServiceCollection();
        var features = new FeaturesCollection();
        _configurator = new UpdatePipelineConfigurator(_services, features);
    }

    private IServiceCollection _services = null!;
    private IUpdatePipelineConfigurator _configurator = null!;

    private class TestCommand : CommandBase<TestCommand>
    {
        public static readonly string ExecutedKey = "Executed";
        private readonly ICommandDescriptorProvider _provider;
        private readonly IServiceProvider _serviceProvider;

        public TestCommand(IServiceProvider serviceProvider, ICommandDescriptorProvider provider)
        {
            _serviceProvider = serviceProvider;
            _provider = provider;
        }

        public override Task ExecuteAsync(UpdateContext context, string[] args)
        {
            context.Items[ExecutedKey] = true;
            return Task.CompletedTask;
        }

        public override void ConfigureDescriptor(ICommandOptionsBuilder builder)
        {
            builder.WithAliases(nameof(TestCommand));
        }
    }

    [Test]
    public void WithCommandBaseTest()
    {
        _configurator.WithCommand<TestCommand>();
        var provider = _configurator.Features.Get<ICommandDescriptorProvider>()!;
        provider.Should().NotBeNull();
        var descriptor = provider.GetCommandDescriptor(nameof(TestCommand));
        descriptor.Should().NotBeNull();
        descriptor!.CommandType.Should().Be<TestCommand>();
        descriptor.Options.Should().NotBeNull();
        descriptor.Options.Aliases.Should().BeEquivalentTo(nameof(TestCommand));
    }

    [Test]
    public void WithCommandDescriptorTest()
    {
        var d = new CommandDescriptor
        {
            Options = new CommandOptions
            {
                Aliases = new[] {nameof(TestCommand)},
            },
            CommandType = typeof(TestCommand),
        };

        _configurator.WithCommand(d);
        var provider = _configurator.Features.Get<ICommandDescriptorProvider>()!;
        provider.Should().NotBeNull();
        var descriptor = provider.GetCommandDescriptor(nameof(TestCommand));
        descriptor.Should().NotBeNull();
        descriptor!.CommandType.Should().Be<TestCommand>();
        descriptor.Options.Should().NotBeNull();
        descriptor.Options.Aliases.Should().BeEquivalentTo(nameof(TestCommand));
    }
}