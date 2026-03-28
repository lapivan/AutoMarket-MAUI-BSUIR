using AutoMarket.Application.CarAnnouncementUseCases.Commands;
using AutoMarket.Application.CarBrandUseCases.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMarket.UI.ViewModels
{
    public partial class AnnouncementDetailsViewModel : ObservableObject
    {
        private readonly IMediator _mediator;
        public AnnouncementDetailsViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [ObservableProperty]
        private AnnouncementDto _announcement;
        [RelayCommand]
        private async Task RemoveAnnouncement() => await RemoveCurrentAnnouncement();
        private async Task RemoveCurrentAnnouncement()
        {
            try
            {
                var command = new DeleteCarAnnouncementCommand(Announcement.Id);
                var result = await _mediator.Send(command);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to remove announcement: {ex.Message}", "OK");
            }
        }
    }
}
