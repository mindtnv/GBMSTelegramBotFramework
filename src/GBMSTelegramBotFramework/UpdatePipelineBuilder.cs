using GBMSTelegramBotFramework.Abstractions;

namespace GBMSTelegramBotFramework;

public class UpdatePipelineBuilder : IUpdatePipelineBuilder
{
    private readonly List<Func<UpdateDelegate, UpdateDelegate>> _middlewares = new();

    public UpdatePipelineBuilder(IServiceProvider services)
    {
        Services = services;
    }

    public IServiceProvider Services { get; }

    public IUpdatePipelineBuilder Use(Func<UpdateDelegate, UpdateDelegate> middleware)
    {
        _middlewares.Add(middleware);
        return this;
    }

    public UpdateDelegate Build()
    {
        UpdateDelegate result = _ => Task.CompletedTask;
        for (var i = _middlewares.Count - 1; i >= 0; i--)
            result = _middlewares[i](result);

        return result;
    }
}