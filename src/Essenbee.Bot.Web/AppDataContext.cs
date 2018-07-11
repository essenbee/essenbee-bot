using Essenbee.Bot.Core;
using Essenbee.Bot.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Essenbee.Bot.Web
{
    public class AppDataContext : DbContext
    {
        public AppDataContext()
        {

        }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }

        public DbSet<StartupMessage> StartupMessages { get; set; }
        public DbSet<TimedMessage> TimedMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["UserSecrets:DatabaseConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
