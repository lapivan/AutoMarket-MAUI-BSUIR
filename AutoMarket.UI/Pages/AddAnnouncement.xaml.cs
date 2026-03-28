using AutoMarket.Application.CarBrandUseCases.Queries;
using AutoMarket.UI.ViewModels;

namespace AutoMarket.UI.Pages;

[QueryProperty(nameof(CarBrandId), "Id")]
public partial class AddAnnouncement : ContentPage
{
	private readonly AddAnnouncementViewModel _viewModel;
    public int CarBrandId
    {
        set
        {
            _viewModel.CarBrandId = value;
        }
    }
    public AddAnnouncement(AddAnnouncementViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}