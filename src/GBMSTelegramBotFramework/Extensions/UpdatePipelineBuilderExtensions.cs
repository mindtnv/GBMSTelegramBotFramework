using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;

namespace GBMSTelegramBotFramework.Extensions;

public static class UpdatePipelineBuilderExtensions
{
    public static IUpdatePipelineBuilder UseHandler<THandler>(this IUpdatePipelineBuilder builder)
        where THandler : IUpdateHandler => builder.UseMiddleware<UpdateHandlerMiddleware<THandler>>();

    public static IUpdatePipelineBuilder UseHandler<THandler>(this IUpdatePipelineBuilder builder, THandler instance)
        where THandler : IUpdateHandler => builder.Use(async (ctx, next) =>
    {
        await instance.HandleUpdateAsync(ctx);
        await next(ctx);
    });

    public static IUpdatePipelineBuilder UseMiddleware<TMiddleware>(this IUpdatePipelineBuilder builder,
        TMiddleware instance)
        where TMiddleware : IUpdateMiddleware => builder.Use(async (ctx, next) =>
    {
        await instance.HandleUpdateAsync(ctx, next);
    });
}