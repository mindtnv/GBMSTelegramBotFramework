using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace GBMSTelegramBotFramework.Commands;

public class CommandMiddleware : IUpdateMiddleware
{
    private readonly ICommandDescriptorProvider _commandDescriptorProvider;
    public static string CommandNameKey => "CommandName";

    public CommandMiddleware(ICommandDescriptorProvider commandDescriptorProvider)
    {
        _commandDescriptorProvider = commandDescriptorProvider;
    }

    public async Task HandleUpdateAsync(UpdateContext context, UpdateDelegate next)
    {
        if (context.Items.ContainsKey(CommandNameKey))
        {
            await next(context);
            return;
        }

        var commandAndArgs = context.Update.Type switch
        {
            UpdateType.Message       => ProcessMessage(context, _commandDescriptorProvider),
            UpdateType.InlineQuery   => null,
            UpdateType.CallbackQuery => ProcessCallbackQuery(context, _commandDescriptorProvider),
            _                        => null,
        };

        if (commandAndArgs == null)
        {
            await next(context);
            return;
        }

        var descriptor = commandAndArgs.CommandDescriptor;
        var command = descriptor.Instance ??
                      context.Services.GetRequiredService(descriptor.CommandType ??
                                                          throw new NullReferenceException(
                                                              nameof(descriptor.CommandType))) as ICommand;
        context.Items[CommandNameKey] = commandAndArgs.Command;
        await command!.ExecuteAsync(context, commandAndArgs.Args);
        if (context.Update.Type == UpdateType.CallbackQuery)
            await context.BotContext.Client.AnswerCallbackQueryAsync(context.Update.CallbackQuery!.Id);
        await next(context);
    }

    private CommandAndArgs? ProcessMessage(UpdateContext context, ICommandDescriptorProvider provider) =>
        context.Update.Message!.Text != null ? ProcessFromText(context.Update.Message!.Text, provider) : null;

    private CommandAndArgs? ProcessCallbackQuery(UpdateContext context, ICommandDescriptorProvider provider) =>
        context.Update.CallbackQuery is {Data: { }}
            ? ProcessFromText(context.Update.CallbackQuery.Data, provider)
            : null;

    private CommandAndArgs? ProcessFromText(string text, ICommandDescriptorProvider provider)
    {
        // Resolve by dictionary
        var tokens = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length > 0)
        {
            var command = tokens[0];
            var descriptor = provider.GetCommandDescriptor(command);
            if (descriptor != null)
            {
                var args = tokens.Length > 1 ? tokens.Skip(1).ToArray() : Array.Empty<string>();
                return new CommandAndArgs(descriptor, command, args);
            }
        }

        // Brute checking for commands with spaces in name
        foreach (var command in provider.Aliases)
        {
            if (!text.StartsWith(command))
                continue;

            var descriptor = provider.GetCommandDescriptor(command);
            if (descriptor == null)
                continue;

            var args = text[command.Length..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new CommandAndArgs(descriptor, command, args);
        }

        return null;
    }

    // private CommandAndArgs? ProcessInlineQuery(UpdateContext context)
    // {
    //     var inlineQuery = context.Update.InlineQuery!;
    //     var tokens = inlineQuery.Query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //     if (tokens.Length > 0)
    //     {
    //         var command = tokens[0];
    //         var args = tokens.Length > 1 ? tokens.Skip(1).ToArray() : Array.Empty<string>();
    //         return new CommandAndArgs(command, args);
    //     }
    //
    //     return null;
    // }

    private class CommandAndArgs
    {
        public string Command { get; }
        public string[] Args { get; }
        public ICommandDescriptor CommandDescriptor { get; }

        public CommandAndArgs(ICommandDescriptor commandDescriptor, string command, string[] args)
        {
            Command = command;
            Args = args;
            CommandDescriptor = commandDescriptor;
        }
    }
}