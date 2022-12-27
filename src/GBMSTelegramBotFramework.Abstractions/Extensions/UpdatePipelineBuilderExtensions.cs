namespace GBMSTelegramBotFramework.Abstractions.Extensions;

public static class UpdatePipelineBuilderExtensions
{
    public static void Use(this IUpdatePipelineBuilder builder, Func<UpdateContext, UpdateDelegate, Task> middleware)
    {
        builder.Use(next => context => middleware(context, next));
    }
}