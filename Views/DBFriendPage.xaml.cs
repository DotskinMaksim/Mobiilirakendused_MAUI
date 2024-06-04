using MAUI_project.Models;

using Microsoft.Maui.Controls;
using static Android.Print.PrintAttributes;

namespace MAUI_project.Views;

public partial class DBFriendPage : ContentPage
{
    public DBFriendPage()
    {
        InitializeComponent();
        Loaded += DBFriendPage_Loaded;
    }

    private void DBFriendPage_Loaded(object sender, EventArgs e)
    {
        Friend friend = (Friend)BindingContext;
    }

    private void SaveFriend(object sender, EventArgs e)
    {
        Friend friend = (Friend)BindingContext;
        if (new string[] { friend.Name, friend.Email, friend.Phone, friend.Address }.All(x => !string.IsNullOrEmpty(x)))
            App.Database.SaveItem(friend);
        Navigation.PopAsync();
    }

    private void DeleteFriend(object sender, EventArgs e)
    {
        Friend friend = (Friend)BindingContext;
        App.Database.DeleteItem(friend.Id);
        Navigation.PopAsync();
    }

    private void Cancel(object sender, EventArgs e) =>
        Navigation.PopAsync();


}