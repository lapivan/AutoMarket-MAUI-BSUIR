using AutoMarket.UI.ViewModels;

namespace AutoMarket.UI.Pages;

public partial class AddCarBrand : ContentPage
{
    public AddCarBrand(AddCarBrandViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}