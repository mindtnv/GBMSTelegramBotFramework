using FluentAssertions;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Commands;
using GBMSTelegramBotFramework.Commands.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Tests.Commands;

[TestFixture("/start", Category = "Commands")]
public class UpdatePipelineOnConfiguratorExtensionsTests
{
    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        var features = new FeaturesCollection();
        _configurator = new UpdatePipelineConfigurator(services, features);
        _configurator.On.Command(_commandName, (_, _) => Task.CompletedTask);
        _provider = _configurator.Features.Get<ICommandDescriptorProvider>()!;
        _provider.Should().NotBeNull();
    }

    private IUpdatePipelineConfigurator _configurator = null!;
    private ICommandDescriptorProvider _provider = null!;
    private readonly string _commandName;

    public UpdatePipelineOnConfiguratorExtensionsTests(string commandName)
    {
        _commandName = commandName;
    }

    [Test]
    public void OnCommandRegistrationTest()
    {
        var descriptor = _provider.GetCommandDescriptor(_commandName);
        descriptor.Should().NotBeNull();
        descriptor!.CommandType.Should().BeNull();
        descriptor.Instance.Should().NotBeNull().And.BeOfType<FuncCommand>();
        descriptor.Options.Should().NotBeNull();
        descriptor.Options.Aliases.Should().BeEquivalentTo(_commandName);
    }
}