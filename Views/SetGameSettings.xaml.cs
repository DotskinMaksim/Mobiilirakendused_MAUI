using System;
using Microsoft.Maui.Controls;

namespace MAUI_project.Views
{
    public partial class SetGameSettingsPage : ContentPage
    {
        public SetGameSettingsPage()
        {
            InitializeComponent();
        }

        private async void StartGame(object sender, EventArgs e)
        {
            string errorMessage = CheckForValidValues();
            if (errorMessage == null)
            {
                await Navigation.PushAsync(new GamePage(player1Entry.Text, player2Entry.Text, Convert.ToInt32(deckSizePicker.SelectedItem), reverseModeSwitch.IsToggled, lowerCardModeSwitch.IsToggled));

            }
            else
            {
                await DisplayAlert("Error", errorMessage, "OK");

            }


        }
        private string CheckForValidValues()
        {
            if (string.IsNullOrWhiteSpace(player1Entry.Text) || string.IsNullOrWhiteSpace(player2Entry.Text))
            {
                return "Please enter names for both players";
                
            }
            if (Convert.ToInt32(deckSizePicker.SelectedItem) == 0)
            {
                return "Please select a deck size";


            }
            if (player1Entry.Text.Length > 20 || player2Entry.Text.Length > 20)
            {
                return "Too long name for player";
            }
            return null;
        }
    }
}
