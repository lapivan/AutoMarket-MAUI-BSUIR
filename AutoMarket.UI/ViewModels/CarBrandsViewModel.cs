using CommunityToolkit.Mvvm.ComponentModel;
using AutoMarket.Application.CarBrandUseCases.Queries;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace AutoMarket.UI.ViewModels;

public partial class CarBrandsViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    public ObservableCollection<BrandListDto> CarBrands { get; set; } = new();
    public ObservableCollection<AnnouncementDto> Announcements { get; set; } = new();
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedBrandInfo))]
    private BrandListDto _selectedBrand;
    public CarBrandsViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    public string SelectedBrandInfo => SelectedBrand == null
        ? string.Empty
        : $"Brand: {SelectedBrand.Name ?? "Unknown"}\n" +
          $"Country: {SelectedBrand.CountryOfOrigin ?? "Unknown"}\n" +
          $"Year of foundation: {SelectedBrand.YearFounded}";
    [RelayCommand]
    public async Task UpdateGroupList() => await GetBrands();
    [RelayCommand]
    public async Task UpdateMembersList() => await GetAnnouncements();
    private async Task GetBrands()
    {
        var brands = await _mediator.Send(new GetCarBrandListQuery());
        if (brands != null)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                CarBrands.Clear();
                foreach (var brand in brands)
                    CarBrands.Add(brand);
            });
        }
    }
    private async Task GetAnnouncements()
    {
        if (SelectedBrand == null) return;

        var brandDetails = await _mediator.Send(new GetCarBrandByIdQuery(SelectedBrand.Id));

        if (brandDetails != null)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Announcements.Clear();
                foreach (var announcement in brandDetails.Announcements)
                    Announcements.Add(announcement);
            });
        }
    }
    partial void OnSelectedBrandChanged(BrandListDto value)
    {
        if (value != null)
            _ = UpdateMembersListCommand.ExecuteAsync(null);
    }
}