using GBMSTelegramBotFramework.Abstractions;
using Moq;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.Tests;

public class TestingUpdateContext : UpdateContext
{
    public TestingUpdateContext(IServiceProvider services, Update update, BotContext botContext,
        ICrossRequestContext crossRequestContext) : base(services, update, botContext, crossRequestContext)
    {
    }

    public TestingUpdateContext() : base(new Mock<IServiceProvider>().Object, new Mock<Update>().Object,
        new Mock<BotContext>().Object, new Mock<ICrossRequestContext>().Object)
    {
    }

    public override void Dispose()
    {
    }
}