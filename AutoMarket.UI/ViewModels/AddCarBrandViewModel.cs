using AutoMarket.Application.CarBrandUseCases.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Diagnostics.Metrics;

namespace AutoMarket.UI.ViewModels
{
    public partial class AddCarBrandViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        [ObservableProperty]
        private string _brandName = String.Empty;

        [ObservableProperty]
        private string _country = String.Empty;

        [ObservableProperty]
        private string _yearFounded = String.Empty;

        public AddCarBrandViewModel(IMediator mediator)
        {
            _mediator = mediator;
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
                var command = new CreateCarBrandCommand(BrandName, Country, year);
                var result = await _mediator.Send(command);
                YearFounded = String.Empty;
                BrandName = String.Empty;
                Country = String.Empty;

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to save brand: {ex.Message}", "OK");
            }
        }
    }
}