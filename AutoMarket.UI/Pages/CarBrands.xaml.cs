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
    private async void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (BindingContext is CarBrandsViewModel vm)
            await vm.UpdateMembersListCommand.ExecuteAsync(null);
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CarBrandsViewModel vm)
        { 
            await vm.UpdateGroupListCommand.ExecuteAsync(null);
            if (vm.SelectedBrand == null)
            {
                vm.Announcements.Clear();
            }
        }
    }
}