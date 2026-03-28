using AutoMarket.Application;
using AutoMarket.Persistence;
using AutoMarket.Persistence.Data;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseMauiCommunityToolkit();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "automarket.db");
            string connectionString = $"Data Source={dbPath}";

            builder.Services.AddApplication();
            builder.Services.AddPersistence(new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connectionString).Options);
            builder.Services.RegisterPages();
            builder.Services.RegisterViewModels();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                //context.Database.EnsureCreated();

                try
                {
                    DbInitializer.Initialize(scope.ServiceProvider).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка при наполнении данными: {ex.Message}");
                }
            }

            return app;
        }
    }
}
