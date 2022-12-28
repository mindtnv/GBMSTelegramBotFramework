using GBMSTelegramBotFramework.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GBMSTelegramBotFramework;

public class UpdateHandlerFactory : IUpdateHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public UpdateHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IUpdateHandler Create(Type updateHandlerType)
    {
        var middleware = _serviceProvider.GetRequiredService(updateHandlerType);
        return (middleware as IUpdateHandler)!;
    }
}