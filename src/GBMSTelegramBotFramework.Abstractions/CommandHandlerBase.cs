namespace GBMSTelegramBotFramework.Abstractions;

public abstract class CommandHandlerBase : UpdateHandlerBase
{
    public static readonly string CommandName = "CommandName";
    public static readonly string CommandArgs = "CommandArgs";
    public static readonly string IsCommandExecuted = "IsCommandExecuted";
    public static string Prefix { get; set; } = "/";
    public abstract string Name { get; }
    public abstract Task ExecuteAsync(UpdateContext context, string[] args);

    public override Task OnMessageAsync(UpdateContext context)
    {
        if (!GetCommandInfoFromMessage(context))
            return Task.CompletedTask;

        if ((context.Items[CommandName] as string ?? throw new ApplicationException())
            .Equals(Name, StringComparison.OrdinalIgnoreCase))
        {
            context.Items[IsCommandExecuted] = true;
            return ExecuteAsync(context, context.Items[CommandArgs] as string[] ?? throw new ApplicationException());
        }

        context.Items[IsCommandExecuted] = false;
        return Task.CompletedTask;
    }

    private static bool GetCommandInfoFromMessage(UpdateContext context)
    {
        if (!context.Items.ContainsKey(CommandName) || !context.Items.ContainsKey(CommandArgs))
        {
            var text = context.Update.Message?.Text;
            if (text == null)
                return false;

            var token = text.AsSpan();
            if (token.Length == 0)
                return false;

            if (!token.StartsWith(Prefix))
                return false;

            token = token.Slice(Prefix.Length);
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