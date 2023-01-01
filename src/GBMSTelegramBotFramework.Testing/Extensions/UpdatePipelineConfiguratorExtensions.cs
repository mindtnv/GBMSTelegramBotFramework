using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;

namespace GBMSTelegramBotFramework.Testing.Extensions;

public static class UpdatePipelineConfiguratorExtensions
{
    public static IUpdatePipelineConfigurator AssertContext(this IUpdatePipelineConfigurator configurator,
        Action<UpdateContext> asserter)
    {
        configurator.Configure(p =>
        {
            p.Use((ctx, next) =>
            {
                asserter(ctx);
                return next(ctx);
            });
        });
        return configurator;
    }
}