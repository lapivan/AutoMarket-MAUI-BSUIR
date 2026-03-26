using AutoMarket.UI.Pages;

namespace AutoMarket.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AnnouncementDetails), typeof(AnnouncementDetails));
        }
    }
}
