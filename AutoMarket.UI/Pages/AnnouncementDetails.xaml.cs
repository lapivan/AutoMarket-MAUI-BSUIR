using AutoMarket.Application.CarBrandUseCases.Queries;
using AutoMarket.UI.ViewModels;

namespace AutoMarket.UI.Pages;

[QueryProperty(nameof(Announcement), "Announcement")]
public partial class AnnouncementDetails : ContentPage
{
    private readonly AnnouncementDetailsViewModel _viewModel;

    public AnnouncementDto Announcement
    {
        set
        {
            _viewModel.Announcement = value;
        }
    }

    public AnnouncementDetails(AnnouncementDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.RefreshCommand.ExecuteAsync(null);
    }
}