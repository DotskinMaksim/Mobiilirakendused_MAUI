using SQLite;
using MAUI_project.Models;
using Kotlin.Time;

namespace MAUI_project.Views;

public partial class EndPage : ContentPage
{
	public EndPage(Player wonPlayer, int playedMoves)
	{
		InitializeComponent();


        Players player = new Players
        {
            Name = "Maksim",
        };
        //App.Database.SaveItem(player);
        //App.Database.DeleteItem(1);
        //App.Database.DeleteItem(2);


        Loaded += PageLoaded;


        victoryLabel.Text = $"Congratulations with victory, {wonPlayer.Name}!";
        movesLabel.Text = $"You played {playedMoves.ToString()} moves";

    }
    private async void WriteDownTheVictory(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("Input your nickname", "Name:");
        string password = await DisplayPromptAsync("Input your password", "Password:");

        IEnumerable<Players> players = new List<Players>();
        players= App.Database.GetItems();

        if (players.Any(player => player.Name == name))
        {
            Players currentPlayer= players.FirstOrDefault(player => player.Name == name);
            if (currentPlayer.Password==password)
            {

                currentPlayer.VictoryCount++;
                currentPlayer.LastWin =DateTime.Now;
                App.Database.SaveItem(currentPlayer);

                await DisplayAlert(
                    "Victory wrote down", $"Your victory wrote down\n" +
                    $"Victory time: {DateTime.Now}\n" +
                    $"Your current victories count: {currentPlayer.VictoryCount} ", "OK");

                writeVictoryButton.IsEnabled = false;
            }
            else
            {
                await DisplayAlert("Error", "Wrong password", "OK");
            }
        }
        else
        {
            Players newPlayer = new Players
            {
                Name=name,
                Password=password,
                VictoryCount=1,
                LastWin=DateTime.Now
            };
            App.Database.SaveItem(newPlayer);

            await DisplayAlert("Victory wrote down", $"Your victory wrote down\n" +
                    $"Victory time: {DateTime.Now}\n" +
                    $"Your current victories count: {newPlayer.VictoryCount} ", "OK");
            writeVictoryButton.IsEnabled = false;

        }
    }
    private async void NewGame(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SetGameSettingsPage());

    }
    private void PageLoaded(object sender, EventArgs e)
    {
        Players player = (Players)BindingContext;
    }
    protected override void OnAppearing()
    {
        playersListView.ItemsSource = App.Database.GetItems();

        base.OnAppearing();
    }



}
