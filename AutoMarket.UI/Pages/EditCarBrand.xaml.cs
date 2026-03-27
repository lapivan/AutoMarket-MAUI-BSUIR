using AutoMarket.Application.CarBrandUseCases.Queries;
using AutoMarket.UI.ViewModels;

namespace AutoMarket.UI.Pages;

public partial class EditCarBrand : ContentPage, IQueryAttributable
{
	public EditCarBrand(EditCarBrandViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Brand") && BindingContext is EditCarBrandViewModel viewModel)
        {
            var brand = query["Brand"] as BrandListDto;
            viewModel.Initialize(brand);
        }
    }
}