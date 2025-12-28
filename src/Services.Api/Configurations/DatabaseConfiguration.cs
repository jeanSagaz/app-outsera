using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.Api.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<ApplicationContext>(options =>
            {
                //options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Infra.Data"));
                options.UseInMemoryDatabase(configuration.GetConnectionString("DefaultConnection")!);

                options.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging();
            });
        }

        public static async Task CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationContext>();
                    await DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
