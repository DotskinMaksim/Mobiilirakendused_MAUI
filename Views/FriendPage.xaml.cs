using MAUI_project.ViewModels;

namespace MAUI_project.Views;

public partial class FriendPage : ContentPage
{
    public FriendViewModel ViewModel { get; private set; }
    public FriendPage(FriendViewModel vm)
    {
        InitializeComponent();
        ViewModel = vm;
        BindingContext = ViewModel;
    }
}