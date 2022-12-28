﻿using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework.Abstractions.Extensions;

public static class UpdatePipelineBuilderExtensions
{
    public static IUpdatePipelineBuilder Use(this IUpdatePipelineBuilder builder,
        Func<UpdateContext, UpdateDelegate, Task> middleware)
    {
        return builder.Use(next => context => middleware(context, next));
    }

    public static IUpdatePipelineBuilder UseMiddleware(this IUpdatePipelineBuilder builder, Type updateMiddlewareType)
    {
        return builder.Use((context, next) =>
        {
            var factory = builder.Services.GetRequiredService<IUpdateMiddlewareFactory>();
            var middleware = factory.Create(updateMiddlewareType);
            return middleware.HandleUpdateAsync(context, next);
        });
    }

    public static IUpdatePipelineBuilder UseMiddleware<TMiddleware>(this IUpdatePipelineBuilder builder)
        where TMiddleware : IUpdateMiddleware =>
        builder.UseMiddleware(typeof(TMiddleware));

    public static IUpdatePipelineBuilder UseHandler<THandler>(this IUpdatePipelineBuilder builder)
        where THandler : IUpdateHandler => builder.UseMiddleware<UpdateHandlerMiddleware<THandler>>();
}