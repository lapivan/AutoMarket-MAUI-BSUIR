using AutoMarket.Application.CarAnnouncementUseCases.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoMarket.UI.ViewModels
{
    public partial class AddAnnouncementViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        [ObservableProperty]
        private int _carBrandId;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _model;

        [ObservableProperty]
        private string _price;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _manufactureYear;
        public AddAnnouncementViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [RelayCommand]
        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                await Shell.Current.DisplayAlert("Error", "Title is required", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Model))
            {
                await Shell.Current.DisplayAlert("Error", "Model is required", "OK");
                return;
            }

            decimal price = 0;
            if (string.IsNullOrWhiteSpace(Price))
            {
                await Shell.Current.DisplayAlert("Error", "Price is required", "OK");
                return;
            }

            if (!decimal.TryParse(Price, out price))
            {
                await Shell.Current.DisplayAlert("Error", "Invalid price format. Please enter a valid number", "OK");
                return;
            }

            if (price <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "Price must be greater than 0", "OK");
                return;
            }

            int manufactureYear = 0;
            if (string.IsNullOrWhiteSpace(ManufactureYear))
            {
                await Shell.Current.DisplayAlert("Error", "Manufacture year is required", "OK");
                return;
            }

            if (!int.TryParse(ManufactureYear, out manufactureYear))
            {
                await Shell.Current.DisplayAlert("Error", "Invalid year format. Please enter a valid year", "OK");
                return;
            }

            int currentYear = DateTime.Now.Year;
            if (manufactureYear < 1900 || manufactureYear > currentYear)
            {
                await Shell.Current.DisplayAlert("Error", $"Manufacture year must be between 1900 and {currentYear}", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                Description = string.Empty;
            }

            try
            {
                var command = new CreateCarAnnouncementCommand(
                    Title,
                    Model,
                    price,
                    Description,
                    manufactureYear,
                    _carBrandId);

                var result = await _mediator.Send(command);

                Title = string.Empty;
                Model = string.Empty;
                Price = string.Empty;
                Description = string.Empty;
                ManufactureYear = string.Empty;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to create announcement: {ex.Message}", "OK");
            }
        }
        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
