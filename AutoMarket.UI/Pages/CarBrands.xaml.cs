using AutoMarket.UI.ViewModels;

namespace AutoMarket.UI.Pages;

public partial class CarBrands : ContentPage
{
	private readonly CarBrandsViewModel _carBrandsViewModel;
    public CarBrands(CarBrandsViewModel carBrandsViewModel)
    {
        InitializeComponent();
        _carBrandsViewModel = carBrandsViewModel;
        BindingContext = _carBrandsViewModel;
    }
    private void PickerSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CarBrandsViewModel vm)
            await vm.UpdateGroupListCommand.ExecuteAsync(null);
    }
}