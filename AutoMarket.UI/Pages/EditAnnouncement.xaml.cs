using AutoMarket.UI.ViewModels;

namespace AutoMarket.UI.Pages;

public partial class EditAnnouncement : ContentPage
{
    public EditAnnouncement(EditAnnouncementViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}