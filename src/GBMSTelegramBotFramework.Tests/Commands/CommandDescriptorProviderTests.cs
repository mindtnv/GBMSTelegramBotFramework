using FluentAssertions;
using GBMSTelegramBotFramework.Commands;
using Moq;

namespace GBMSTelegramBotFramework.Tests.Commands;

[TestFixture("/start", "start", Category = "Commands")]
public class CommandDescriptorProviderTests
{
    [SetUp]
    public void SetUp()
    {
        var descriptor = new CommandDescriptor
        {
            Options = new CommandOptions
            {
                Aliases = new[] {_firstAlias, _secondAlias},
            },
            CommandType = typeof(ICommand),
        };
        _provider = new CommandDescriptorProvider();
        _provider.AddCommandDescriptor(descriptor);
    }

    private ICommandDescriptorProvider _provider = null!;
    private readonly string _firstAlias;
    private readonly string _secondAlias;

    public CommandDescriptorProviderTests(string firstAlias, string secondAlias)
    {
        _firstAlias = firstAlias;
        _secondAlias = secondAlias;
    }

    [Test]
    public void NullOnNotFoundDescriptorTest()
    {
        var descriptor = _provider.GetCommandDescriptor("/notfound");
        descriptor.Should().BeNull();
    }

    [Test]
    public void RegisterCommandDescriptorTest()
    {
        var descriptor = _provider.GetCommandDescriptor(_firstAlias);
        descriptor.Should().NotBeNull();
        descriptor!.CommandType.Should().Be<ICommand>();
        descriptor.Options.Should().NotBeNull();
        descriptor.Options.Aliases.Should().BeEquivalentTo(_firstAlias, _secondAlias);
    }

    [Test]
    public void RewriteCommandDescriptorTest()
    {
        var newDescriptor = new CommandDescriptor
        {
            Options = new CommandOptions
            {
                Aliases = new[] {_firstAlias},
            },
            Instance = new Mock<ICommand?>().Object,
        };
        _provider.AddCommandDescriptor(newDescriptor);
        _provider.Aliases.Should().BeEquivalentTo(_firstAlias, _secondAlias);
        var d1 = _provider.GetCommandDescriptor(_firstAlias);
        d1.Should().NotBeNull();
        d1!.CommandType.Should().BeNull();
        d1.Options.Should().NotBeNull();
        d1.Options.Aliases.Should().BeEquivalentTo(_firstAlias);
        var d2 = _provider.GetCommandDescriptor(_secondAlias);
        d2.Should().NotBeNull();
        d2!.CommandType.Should().Be<ICommand>();
        d2.Options.Should().NotBeNull();
        d2.Options.Aliases.Should().BeEquivalentTo(_firstAlias, _secondAlias);
    }
}