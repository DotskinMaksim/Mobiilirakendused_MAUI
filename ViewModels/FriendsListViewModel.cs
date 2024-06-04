using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MAUI_project.Models;
using MAUI_project.Views;
using MAUI_project.ViewModels;

namespace MAUI_project.ViewModels
{
    public class FriendsListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FriendViewModel> Friends { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreateFriendCommand { get; protected set; }
        public ICommand DeleteFriendCommand { get; protected set; }
        public ICommand SaveFriendCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        FriendViewModel selectedFriend;
        public INavigation Navigation { get; set; }
        public FriendsListViewModel()
        {
            Friends = new ObservableCollection<FriendViewModel>();
            CreateFriendCommand = new Command(CreateFriend);
            DeleteFriendCommand = new Command(DeleteFriend);
            SaveFriendCommand = new Command(SaveFriend);
            BackCommand = new Command(Back);
        }
        public FriendViewModel SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                if (selectedFriend == value) return;
                FriendViewModel temp = value;
                selectedFriend = null;
                OnPropertyChanged("SelectedFriend");
                Navigation.PushAsync(new FriendPage(temp));
                
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void CreateFriend() => Navigation.PushAsync(new FriendPage(new FriendViewModel() { FriendsListViewModel = this }));
        private void Back() => Navigation.PopAsync();
        private void SaveFriend(object friendObj)
        {
            if (friendObj is not FriendViewModel friend || friend == null || !friend.IsValid || Friends.Contains(friend)) return;
            Friends.Add(friend);
            Back();
        }
        private void DeleteFriend(object friendObj)
        {
            if (friendObj is not FriendViewModel friend || friend == null) return;
            Friends.Remove(friend);
            Back();
        }

    }
}