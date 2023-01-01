using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.Abstractions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace GBMSTelegramBotFramework.AspNetCore;

public class UpdateController : ControllerBase
{
    private readonly IBotProvider _botProvider;

    public UpdateController(IBotProvider botProvider)
    {
        _botProvider = botProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromRoute] string name, [FromBody] Update update)
    {
        var bot = _botProvider.GetBot(name);
        await bot.HandleUpdateAsync(update);
        return Ok();
    }
}