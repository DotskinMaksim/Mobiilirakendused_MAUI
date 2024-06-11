using MAUI_project.Views;
using MAUI_project.Models;
namespace MAUI_project
{
    public partial class MainPage : ContentPage
    {
        public static ContentPage SettingsPage { get; set; }

        public MainPage()
        {
            InitializeComponent();
            SettingsPage = new SetGameSettingsPage();

        }

        private async void StartButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(SettingsPage);
            //await Navigation.PushAsync(new EndPage());
        }


    }
}
