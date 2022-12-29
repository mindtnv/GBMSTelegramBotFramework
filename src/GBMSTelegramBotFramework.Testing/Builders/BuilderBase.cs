namespace GBMSTelegramBotFramework.Testing.Builders;

public abstract class BuilderBase
{
    public TBuilder Configure<TBuilder>(Action<TBuilder> configure) where TBuilder : BuilderBase
    {
        var builder = (TBuilder) this;
        configure(builder);
        return builder;
    }
}