# GBMS Telegram Bot Framework 

**GBMS Telegram Bot Framework** is a simple framework for creating and hosting Telegram bots. 
It is based on [Telegram Bot API](https://core.telegram.org/bots/api) and [Telegran.Bot](https://github.com/TelegramBots/Telegram.Bot).

## Supported Platforms

Project targets .NET Standard 2.1 (GBMSTelegramBotFramework.AspNetCore - net7.0).

## Example

### Basic bot with long polling

```csharp
using GBMSTelegramBotFramework.Commands.Extensions;
using GBMSTelegramBotFramework.Extensions;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder();
builder.ConfigureServices(services =>
       {
           // Use long polling for receiving updates
           services.UseTelegramLongPulling();
           services.AddTelegramBot(bot =>
           {
               bot.ConfigureOptions(o =>
                   o.WithName("example-bot").WithToken("bot-token"));

               bot.On.Command("/start", (ctx, args) => 
                   ctx.Reply.WithText("Hello world!"));
               
               bot.On.Photo(ctx => ctx.Reply.WithText("Nice photo!"));
               bot.UseCommands();
           });
       })
       .ConfigureDefaults(args);

var app = builder.Build();
await app.RunAsync();
```

### Basic bot with webhook

```csharp
using GBMSTelegramBotFramework.AspNetCore;
using GBMSTelegramBotFramework.Commands.Extensions;
using GBMSTelegramBotFramework.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();

// Use and configure webhook for receiving updates
builder.Services.UseTelegramWebHook(o => o.Url = "https://035d-37-112-72-42.eu.ngrok.io/");
builder.Services.AddTelegramBot(bot =>
{
    bot.ConfigureOptions(o =>
        o.WithName("example-bot").WithToken("bot-token"));

    bot.On.Command("/start", (ctx, args) =>
        ctx.Reply.WithText("Hello world!"));

    bot.On.Photo(ctx => ctx.Reply.WithText("Nice photo!"));
    bot.UseCommands();
});

var app = builder.Build();
app.MapWebhookUpdateController();
app.Run();
```

### Commands

This example shows how to create a bot with two commands: `/start` and `/process`.

```csharp
builder.Services.AddTelegramBot(bot =>
{
    bot.ConfigureOptions(o =>
        o.WithName("e-bot").WithToken("bot-token"));

    bot.On.Command("/start", (ctx, args) => ctx.Reply.WithText("Hello world!"));
    bot.On.Command("/process", (ctx, args) => ctx.Reply.WithText("Processing..."));
    bot.UseCommands();
});
```

#### Class based commands

This example shows how to create commands using classes and register them in the bot.

```csharp
class StartCommand : CommandBase<StartCommand>
{
    public override void ConfigureDescriptor(ICommandOptionsBuilder builder)
    {
        builder.WithAliases("/start", "start");
    }

    public override Task ExecuteAsync(UpdateContext context, string[] args) =>
        context.Reply.WithText("Args: " + string.Join(", ", args));
}
```

### States

This example shows hot to create a bot with two states: `main` and `process` and how to leave and enter states.

```csharp
builder.Services.AddTelegramBot(bot =>
{
    bot.ConfigureOptions(o =>
        o.WithName("e-bot").WithToken("bot-token"));

    bot.On.Command("/start", (ctx, args) => ctx.EnterStateAsync("main"));
    bot.WithState(state =>
    {
        state.WithName("main")
             .Initial()
             .OnEnter(ctx => ctx.Reply.WithText("Main menu"));

        state.On.Command("/process", (ctx, _) => ctx.EnterStateAsync("process"));
        state.UseCommands();
    });

    bot.WithState(state =>
    {
        state.WithName("process")
             .OnEnter(ctx => ctx.Reply.WithText("Processing... Enter something to finish"));
        
        state.On.Text(ctx => ctx.EnterStateAsync("main"));
    });
    
    bot.UseCommands();
    bot.UseStates();
});
```

#### Class based states

This example shows how to create states using classes and register them in the bot.

```csharp
class MainState : BotStateBase
{
    private readonly ILogger<MainState> _logger;

    public MainState(ILogger<MainState> logger)
    {
        _logger = logger;
    }

    public override string Name => "main";
    public override bool IsInitial => true;

    public override void ConfigureUpdatePipeline(IUpdatePipelineConfigurator configurator)
    {
        configurator.On.Command("/process", (ctx, _) =>
            ctx.EnterStateAsync("process"));
        configurator.UseCommands();
    }

    public override async Task OnEnterAsync(UpdateContext context)
    {
        _logger.LogInformation("User {Id} entered main state", context.Update.GetFromId());
        await context.Reply.WithText("Main menu");
    }
}
```

```csharp
builder.Services.AddTelegramBot(bot =>
{
    bot.ConfigureOptions(o =>
        o.WithName("e-bot").WithToken("bot-token"));

    bot.On.Command("/start", (ctx, args) => ctx.EnterStateAsync("main"));
    bot.WithState<MainState>();
    bot.WithState<ProcessState>();
    bot.UseCommands();
    bot.UseStates();
});
```

### Commands

This example shows how to create a bot with two commands: `/start` and `/process`.

```csharp




