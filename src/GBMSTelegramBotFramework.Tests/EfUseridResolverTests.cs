using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using GBMSTelegramBotFramework.EntityFramework;
using GBMSTelegramBotFramework.EntityFramework.Extensions;
using GBMSTelegramBotFramework.Extensions;
using GBMSTelegramBotFramework.Testing.Builders;
using GBMSTelegramBotFramework.Testing.Extensions;
using GBMSTelegramBotFramework.Tests.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace GBMSTelegramBotFramework.Tests;

[TestFixture]
public class EfUseridResolverTests
{
    [Test]
    public async Task InMemoryIntegrationTest()
    {
        var services = new ServiceCollection();
        services.AddTelegramBot(bot =>
        {
            bot.ConfigureOptions(o => o.WithName("test-bot"));
            bot.UseMiddleware<ChatIdResolverMiddleware>();
            bot.UseEfChatIdResolver<TestDbBotContext>();
            bot.UseCommand<StartCommandHandler>();
            bot.UseCommand<SendMessageToUserIdCommand>();
            bot.UseTestingClient();
        });
        services.AddDbContext<TestDbBotContext>(o => o.UseInMemoryDatabase("test-db"));
        var provider = services.BuildServiceProvider();
        var context = provider.GetRequiredService<DbBotContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        var bot = provider.GetRequiredService<IBot>();
        var update1 = new UpdateBuilder().WithMessage(m =>
                                         {
                                             m.From(u => u.WithId(111));
                                             m.WithChat(c => c.WithId(111000));
                                             m.WithText("/start");
                                         })
                                         .Build();
        var update2 = new UpdateBuilder().WithMessage(m =>
                                         {
                                             m.From(u => u.WithId(111));
                                             m.WithChat(c => c.WithId(111000));
                                             m.WithText("/test");
                                         })
                                         .Build();
        await bot.HandleUpdateAsync(update1);
        await bot.HandleUpdateAsync(update2);
        bot.Assert(x =>
        {
            x.ShouldSendMessageWithText(StartCommandHandler.Message, 111000).GoToNextRequest();
            x.ShouldSendMessageWithText(SendMessageToUserIdCommand.Message, 111000);
        });
    }
}

public class SendMessageToUserIdCommand : CommandHandlerBase
{
    public static string Message = "Hello World!";
    private readonly IChatIdResolverStore _resolverStore;
    public override string Name => "test";

    public SendMessageToUserIdCommand(IChatIdResolverStore resolverStore)
    {
        _resolverStore = resolverStore;
    }

    public override async Task ExecuteAsync(UpdateContext context, string[] args)
    {
        var chatIdResolver = _resolverStore.GetResolver(context.BotContext.Options.Name!);
        var chatId = await chatIdResolver.GetChatIdAsync(context.Update.GetFromId()!.Value);
        await context.BotContext.Client.SendTextMessageAsync(chatId, Message);
    }
}

public class TestDbBotContext : DbBotContext
{
    public TestDbBotContext(DbContextOptions<TestDbBotContext> options) : base(options)
    {
    }
}