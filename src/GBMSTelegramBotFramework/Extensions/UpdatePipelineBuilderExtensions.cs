using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;

namespace GBMSTelegramBotFramework.Extensions;

public static class UpdatePipelineBuilderExtensions
{
    public static IUpdatePipelineBuilder UseHandler<THandler>(this IUpdatePipelineBuilder builder)
        where THandler : IUpdateHandler => builder.UseMiddleware<UpdateHandlerMiddleware<THandler>>();
}