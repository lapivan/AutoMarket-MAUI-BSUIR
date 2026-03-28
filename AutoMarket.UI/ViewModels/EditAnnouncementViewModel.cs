using AutoMarket.Application.CarAnnouncementUseCases.Commands;
using AutoMarket.Application.CarAnnouncementUseCases.Queries;
using AutoMarket.Application.CarBrandUseCases.Queries;
using AutoMarket.Domain.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AutoMarket.UI.ViewModels
{
    [QueryProperty(nameof(Id), "Id")]
    public partial class EditAnnouncementViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        [ObservableProperty] private int _id;
        [ObservableProperty] private string _title;
        [ObservableProperty] private string _model;
        [ObservableProperty] private string _price;
        [ObservableProperty] private string _description;
        [ObservableProperty] private string _manufactureYear;
        [ObservableProperty] private BrandListDto _selectedBrand;

        public ObservableCollection<BrandListDto> Brands { get; } = new();

        public EditAnnouncementViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        partial void OnIdChanged(int value) => LoadData();

        private async void LoadData()
        {
            var brands = await _mediator.Send(new GetCarBrandListQuery());
            Brands.Clear();
            foreach (var brand in brands) Brands.Add(brand);

            var announcement = await _mediator.Send(new GetCarAnnouncementByIdQuery(Id));
            if (announcement != null)
            {
                Title = announcement.Title;
                Model = announcement.Model;
                Price = announcement.Price.ToString();
                Description = announcement.Description;
                ManufactureYear = announcement.ManufactureYear.ToString();
                SelectedBrand = Brands.FirstOrDefault(b => b.Id == announcement.CarBrandId);
            }
        }

        [RelayCommand]
        public async Task Save()
        {
            if (!decimal.TryParse(Price, out var priceValue) || !int.TryParse(ManufactureYear, out var yearValue))
            {
                await Shell.Current.DisplayAlert("Error", "Check numeric fields", "OK");
                return;
            }

            var command = new UpdateCarAnnouncementCommand(
                Id,
                Title,
                Model,
                decimal.Parse(Price),
                Description,
                int.Parse(ManufactureYear),
                AnnouncementStatus.Active,
                SelectedBrand.Id
    );

            var result = await _mediator.Send(command);

            if (result)
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async Task Cancel() => await Shell.Current.GoToAsync("..");
    }
}