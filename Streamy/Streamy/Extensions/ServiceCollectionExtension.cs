

using Microsoft.EntityFrameworkCore;
using Streamy.Core.Contracts;
using Streamy.Core.Services;
using Streamy.Infrastructure.Data;
using Streamy.Infrastructure.Data.Repositories;

namespace Streamy.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ISongService, SongService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
