using AutoMarket.Application.CarBrandUseCases.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMarket.UI.ViewModels
{
    public partial class AnnouncementDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private AnnouncementDto _announcement;
    }
}
