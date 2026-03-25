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
            services.AddSingleton<IUnitOfWork, FakeUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, DbContextOptions<AppDbContext> options)
        {
            services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
            services.AddSingleton(new AppDbContext(options));
            return services;
        }
    }
}
