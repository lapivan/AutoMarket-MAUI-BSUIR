using AutoMarket.Application.CarAnnouncementUseCases.Commands;
using AutoMarket.Application.CarAnnouncementUseCases.Queries;
using AutoMarket.Application.CarBrandUseCases.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            string path = ImagePathHelper.GetImagePath(Announcement.Id);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Не удалось удалить файл: {ex.Message}");
                }
            }
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
        [RelayCommand]
        private async Task EditAnnouncement()
        {
            await Shell.Current.GoToAsync($"{nameof(EditAnnouncement)}?Id={Announcement.Id}");
        }

        [RelayCommand]
        private async Task ChangeImage()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Select Car Image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result == null) return;

                string targetPath = ImagePathHelper.GetImagePath(Announcement.Id);

                using (var stream = await result.OpenReadAsync())
                using (var newStream = File.OpenWrite(targetPath))
                {
                    await stream.CopyToAsync(newStream);
                }

                var temp = Announcement;
                Announcement = null;
                Announcement = temp;

                await Shell.Current.DisplayAlert("Success", "Image updated!", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to save image: {ex.Message}", "OK");
            }
        }
        [RelayCommand]
        public async Task Refresh()
        {
            if (Announcement == null) return;
            var freshData = await _mediator.Send(new GetCarAnnouncementByIdQuery(Announcement.Id));
            if (freshData != null)
            {
                Announcement = freshData;
            }
        }
    }
}
