using AutoMarket.Application.CarBrandUseCases.Commands;
using AutoMarket.Application.CarBrandUseCases.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Diagnostics.Metrics;

namespace AutoMarket.UI.ViewModels
{
    public partial class EditCarBrandViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        private int _brandId;

        [ObservableProperty]
        private string _brandName = string.Empty;

        [ObservableProperty]
        private string _country = string.Empty;

        [ObservableProperty]
        private string _yearFounded = string.Empty;

        public EditCarBrandViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Initialize(BrandListDto brand)
        {
            _brandId = brand.Id;
            BrandName = brand.Name ?? string.Empty;
            Country = brand.CountryOfOrigin ?? string.Empty;
            YearFounded = brand.YearFounded.ToString() ?? string.Empty;
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(BrandName))
            {
                await Shell.Current.DisplayAlert("Error", "Brand name is required", "OK");
                return;
            }

            int year = 0;
            if (!string.IsNullOrWhiteSpace(YearFounded))
            {
                if (!int.TryParse(YearFounded, out int parsedYear))
                {
                    await Shell.Current.DisplayAlert("Error", "Invalid year format", "OK");
                    return;
                }
                year = parsedYear;
            }

            try
            {
                var command = new UpdateCarBrandCommand(_brandId, BrandName, Country, year);
                await _mediator.Send(command);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to update brand: {ex.Message}", "OK");
            }
        }
    }
}