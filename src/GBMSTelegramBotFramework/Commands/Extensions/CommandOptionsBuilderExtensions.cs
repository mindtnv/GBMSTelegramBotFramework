namespace GBMSTelegramBotFramework.Commands.Extensions;

public static class CommandOptionsBuilderExtensions
{
    public static ICommandOptionsBuilder WithAliases(this ICommandOptionsBuilder builder, params string[] aliases)
    {
        builder.WithAliases(aliases);
        return builder;
    }
}