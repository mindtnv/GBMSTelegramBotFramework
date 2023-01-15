using GBMSTelegramBotFramework.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace GBMSTelegramBotFramework.EntityFramework;

public class DbBotContext : DbContext
{
    public DbSet<UserIdCorrelation> UserIdCorrelations { get; set; }

    protected DbBotContext(DbContextOptions options) : base(options)
    {
    }
}