using Essenbee.Bot.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Essenbee.Bot.Web
{
    public class AppDataContext : DbContext
    {
        private readonly IOptions<UserSecrets> _config;

        public AppDataContext()
        {

        }

        public AppDataContext(IOptions<UserSecrets> config, DbContextOptions<AppDataContext> options) : base(options)
        {
            _config = config;
        }

        public DbSet<StartupMessage> StartupMessages { get; set; }
        public DbSet<TimedMessage> TimedMessages { get; set; }
        public DbSet<ProjectText> ProjectText { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = string.Empty;

            if (_config is null)
            {
                connectionString = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()["UserSecrets:DatabaseConnectionString"];
            }
            else
            {
                connectionString = _config.Value.DatabaseConnectionString;
            }

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
