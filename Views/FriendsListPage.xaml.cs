using MAUI_project.ViewModels;

namespace MAUI_project.Views
{

    public partial class FriendsListPage : ContentPage
    {
        public FriendsListPage()
        {
            InitializeComponent();
            BindingContext = new FriendsListViewModel { Navigation = Navigation };

        }
    }
}