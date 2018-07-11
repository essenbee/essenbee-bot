using Essenbee.Bot.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Essenbee.Bot.Web
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        public DbSet<StartupMessage> StartupMessages { get; set; }
        public DbSet<TimedMessage> TimedMessages { get; set; }
    }
}
