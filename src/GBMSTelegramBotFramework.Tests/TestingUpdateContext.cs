using GBMSTelegramBotFramework.Abstractions;
using Moq;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Tests;

public class TestingUpdateContext : UpdateContext
{
    public TestingUpdateContext(IServiceProvider services, Update update, BotContext botContext,
        IFeaturesCollection features) : base(services, update, botContext, features)
    {
    }

    public TestingUpdateContext() : base(new Mock<IServiceProvider>().Object, new Mock<Update>().Object,
        new Mock<BotContext>().Object, new Mock<IFeaturesCollection>().Object)
    {
    }

    public override void Dispose()
    {
    }
}