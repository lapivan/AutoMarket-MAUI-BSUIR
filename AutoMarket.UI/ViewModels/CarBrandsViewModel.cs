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
    private BrandListDto _selectedBrand;
    public CarBrandsViewModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    [RelayCommand]
    public async Task UpdateGroupList() => await GetBrands();
    [RelayCommand]
    public async Task UpdateMembersList() => await GetAnnouncements();

    public async Task GetBrands()
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
    public async Task GetAnnouncements()
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
}