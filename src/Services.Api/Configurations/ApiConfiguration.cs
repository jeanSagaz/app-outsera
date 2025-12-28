using Application.Interfaces;
using Application.Services;
using Core.Notifications;
using Core.Notifications.Interfaces;
using Domain.Interfaces;
using Infra.Data.Repository;

namespace Services.Api.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddScoped<IDomainNotifier, DomainNotifier>();

            // Application
            services.AddScoped<IMovieService, MovieService>();

            // Infra - Data
            services.AddScoped<IMovieRepository, MovieRepository>();
        }
    }
}
