namespace GBMSTelegramBotFramework.Abstractions;

public abstract class CommandHandlerBase : UpdateHandlerBase
{
    private static readonly string CommandName = "CommandName";
    private static readonly string CommandArgs = "CommandArgs";
    public abstract string Name { get; }
    public abstract Task ExecuteAsync(UpdateContext context, string[] args);

    public override Task OnMessageAsync(UpdateContext context)
    {
        if (!GetCommandInfoFromMessage(context))
            return Task.CompletedTask;

        return (context.Items[CommandName] as string ?? throw new ApplicationException())
            .Equals(Name, StringComparison.OrdinalIgnoreCase)
                ? ExecuteAsync(context, context.Items[CommandArgs] as string[] ?? throw new ApplicationException())
                : Task.CompletedTask;
    }

    private static bool GetCommandInfoFromMessage(UpdateContext context)
    {
        if (!context.Items.ContainsKey(CommandName) || !context.Items.ContainsKey(CommandArgs))
        {
            var text = context.Update.Message?.Text;
            if (text == null)
                return false;

            var token = text.AsSpan();
            if (token.Length == 0 || token[0] != '/')
                return false;

            token = token[1..];
            var haveArgs = token.IndexOf(' ') is var argsIndex && argsIndex > 0;
            var args = Array.Empty<string>();
            string command;
            if (haveArgs)
            {
                command = token[..argsIndex].ToString();
                args = token[(argsIndex + 1)..].ToString().Split(' ');
            }
            else
            {
                command = token.ToString();
            }

            context.Items[CommandName] = command;
            context.Items[CommandArgs] = args;
            return true;
        }

        return true;
    }
}