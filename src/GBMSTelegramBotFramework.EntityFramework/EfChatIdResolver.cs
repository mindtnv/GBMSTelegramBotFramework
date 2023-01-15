using System.Diagnostics;
using GBMSTelegramBotFramework.Abstractions;
using GBMSTelegramBotFramework.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace GBMSTelegramBotFramework.EntityFramework;

public class EfChatIdResolver : IChatIdResolver
{
    private readonly string _botName;
    private readonly DbBotContext _context;
    private readonly IChatIdResolver _hash = new InMemoryChatIdResolver();

    public EfChatIdResolver(DbBotContext context, string botName)
    {
        _context = context;
        _botName = botName;
    }

    public async Task<long> GetChatIdAsync(long userId)
    {
        if (await _hash.ContainsCorrelationAsync(userId))
        {
            Debug.Print($"Find chatId for {userId} in hash");
            return await _hash.GetChatIdAsync(userId);
        }

        var correlation = await _context.UserIdCorrelations.FirstOrDefaultAsync(x =>
            x.UserId == userId && x.BotName == _botName);

        if (correlation == null)
            throw new EfChatIdResolverException("ChatId cannot be resolved for UserId: " + userId, userId);

        await _hash.AddCorrelationAsync(userId, correlation!.ChatId);
        return await _hash.GetChatIdAsync(userId);
    }

    public async Task AddCorrelationAsync(long chatId, long userId)
    {
        await _hash.AddCorrelationAsync(userId, chatId);
        await _context.UserIdCorrelations.AddAsync(new UserIdCorrelation
        {
            UserId = chatId,
            ChatId = userId,
            BotName = _botName,
        });
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ContainsCorrelationAsync(long userId)
    {
        if (await _hash.ContainsCorrelationAsync(userId))
            return true;

        var correlation =
            await _context.UserIdCorrelations.FirstOrDefaultAsync(x => x.UserId == userId && x.BotName == _botName);

        if (correlation == null)
            return false;

        await _hash.AddCorrelationAsync(userId, correlation!.ChatId);
        return true;
    }
}