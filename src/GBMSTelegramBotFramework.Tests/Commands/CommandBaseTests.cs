using FluentAssertions;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Commands;
using GBMSTelegramBotFramework.Commands.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Tests.Commands;

[TestFixture("/test", Category = "Commands")]
public class CommandBaseTests
{
    [SetUp]
    public void SetUp()
    {
        _services = new ServiceCollection();
        var features = new FeaturesCollection();
        _configurator = new UpdatePipelineConfigurator(_services, features);
        _configurator.WithCommand(new TestCommand(_commandAlias));
        _provider = _configurator.Features.Get<ICommandDescriptorProvider>()!;
        _provider.Should().NotBeNull();
    }

    private IServiceCollection _services = null!;
    private IUpdatePipelineConfigurator _configurator = null!;
    private ICommandDescriptorProvider _provider = null!;
    private readonly string _commandAlias;

    public CommandBaseTests(string commandAlias)
    {
        _commandAlias = commandAlias;
    }

    private class TestCommand : CommandBase<TestCommand>
    {
        public static readonly string ExecutedKey = "Executed";
        private readonly string _commandAlias;

        public TestCommand(string commandAlias)
        {
            _commandAlias = commandAlias;
        }

        public override Task ExecuteAsync(UpdateContext context, string[] args)
        {
            context.Items[ExecutedKey] = true;
            return Task.CompletedTask;
        }

        public override void ConfigureDescriptor(ICommandOptionsBuilder builder)
        {
            builder.WithAliases(_commandAlias);
        }
    }

    [Test]
    public void WorkAsCommandDescriptor()
    {
        var descriptor = _provider.GetCommandDescriptor(_commandAlias);
        descriptor.Should().NotBeNull();
        descriptor!.CommandType.Should().Be<TestCommand>();
        descriptor.Options.Should().NotBeNull();
        descriptor.Options.Aliases.Should().BeEquivalentTo(_commandAlias);
    }
}