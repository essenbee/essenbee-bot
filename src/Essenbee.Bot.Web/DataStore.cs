using Essenbee.Bot.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Essenbee.Bot.Web
{
    public static class DataStore
    {
        public static IRepository Setup(string connectionString)
        {
            DbContextOptions<AppDataContext> options = new DbContextOptionsBuilder<AppDataContext>()
                .UseSqlServer(connectionString)
                .Options;

            var appDataContext = new AppDataContext(options);
            EnsureDatabase(appDataContext);
            IRepository repository = new EntityFrameworkRepository(appDataContext);
            return repository;
        }

        private static void EnsureDatabase(AppDataContext dataContext)
        {
            dataContext.Database.Migrate();
        }
    }
}
