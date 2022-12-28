using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework;

public class UpdateMiddlewareFactory : IUpdateMiddlewareFactory
{
    private readonly IServiceProvider _serviceProvider;

    public UpdateMiddlewareFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IUpdateMiddleware Create(Type updateMiddlewareType)
    {
        var middleware = _serviceProvider.GetRequiredService(updateMiddlewareType);
        return (middleware as IUpdateMiddleware)!;
    }
}