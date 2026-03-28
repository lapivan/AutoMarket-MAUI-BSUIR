using AutoMarket.UI.Pages;
using AutoMarket.UI.ViewModels;

namespace AutoMarket.UI
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPages(this IServiceCollection services)
        {
            services.AddTransient<CarBrands>()
                    .AddTransient<AnnouncementDetails>()
                    .AddTransient<AddCarBrand>()
                    .AddTransient<EditCarBrand>()
                    .AddTransient<AddAnnouncement>();
            return services;
        }
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddTransient<CarBrandsViewModel>()
                    .AddTransient<AnnouncementDetailsViewModel>()
                    .AddTransient<AddCarBrandViewModel>()
                    .AddTransient<EditCarBrandViewModel>()
                    .AddTransient<AddAnnouncementViewModel>();
            return services;
        }
    }
}
