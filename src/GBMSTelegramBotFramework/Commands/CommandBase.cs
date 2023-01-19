using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework.Commands;

public abstract class CommandBase<TCommand> : ICommand, ICommandDescriptor where TCommand : ICommand
{
    public abstract Task ExecuteAsync(UpdateContext context, string[] args);
    Type ICommandDescriptor.CommandType => typeof(TCommand);
    CommandOptions ICommandDescriptor.Options
    {
        get
        {
            var builder = new CommandOptionsBuilder();
            ConfigureDescriptor(builder);
            return builder.Build();
        }
    }
    ICommand? ICommandDescriptor.Instance => null;
    public abstract void ConfigureDescriptor(ICommandOptionsBuilder builder);
}