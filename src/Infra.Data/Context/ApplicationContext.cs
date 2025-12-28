using Domain.Models;
using Infra.Data.Handler;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {            

        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(conf =>
            {
                conf.HasKey(p => p.Id);
                
                conf.ToTable("Movies");
            });

            base.OnModelCreating(modelBuilder);
        }
    }

    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationContext context)
        {
            var moviesCsv = CsvHandler.ReaderCsv();

            if (moviesCsv is not null)
            {
                var movies = moviesCsv.Select(c => new Movie(c.Year, c.Title, c.Studio, c.Producer, c.Winner != null && c.Winner.Equals("yes") ? true : null));

                await context.AddRangeAsync(movies);
                context.SaveChanges();
            }
        }
    }
}
