using AutoMarket.Persistence.Data;
using AutoMarket.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMarket.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFakePersistence(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, FakeUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, DbContextOptions options)
        {
            services.AddPersistence();
            services.AddScoped<AppDbContext>(provider =>
                new AppDbContext((DbContextOptions<AppDbContext>)options));

            return services;
        }
    }
}