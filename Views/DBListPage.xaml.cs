using MAUI_project.Models;


namespace MAUI_project.Views;

public partial class DBListPage : ContentPage
{
    public DBListPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        friendsList.ItemsSource = App.Database.GetItems();
        base.OnAppearing();
    }
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Friend selectedFriend = e.SelectedItem as Friend;
        DBFriendPage friendPage = new DBFriendPage();
        friendPage.BindingContext = selectedFriend;
        await Navigation.PushAsync(friendPage);
    }

    private async void CreateFriend(object sender, EventArgs e)
    {
        Friend friend = new Friend();
        DBFriendPage friendPage = new DBFriendPage();
        friendPage.BindingContext = friend;
        await Navigation.PushAsync(friendPage);
    }
    [Obsolete]
    private void ShowName(object sender, EventArgs e)
    {
        friendsList.ItemTemplate = new DataTemplate(() =>
        {
            Label labelList = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            labelList.SetBinding(Label.TextProperty, "Name");
            return new ViewCell { View = new HorizontalStackLayout { Children = { labelList } } };
        });
    }
    [Obsolete]
    private void ShowEmail(object sender, EventArgs e)
    {
        friendsList.ItemTemplate = new DataTemplate(() =>
        {
            Label labelList = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            labelList.SetBinding(Label.TextProperty, "Email");
            return new ViewCell { View = new HorizontalStackLayout { Children = { labelList } } };
        });
    }
    [Obsolete]
    private void ShowPhone(object sender, EventArgs e)
    {
        friendsList.ItemTemplate = new DataTemplate(() =>
        {
            Label labelList = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            labelList.SetBinding(Label.TextProperty, "Phone");
            return new ViewCell { View = new HorizontalStackLayout { Children = { labelList } } };
        });
    }
    [Obsolete]
    private void ShowAddress(object sender, EventArgs e)
    {
        friendsList.ItemTemplate = new DataTemplate(() =>
        {
            Label labelList = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            labelList.SetBinding(Label.TextProperty, "Address");
            return new ViewCell { View = new HorizontalStackLayout { Children = { labelList } } };
        });
    }
    
}