using System.ComponentModel;
using MAUI_project.Models;
namespace MAUI_project.ViewModels
{
    public class FriendViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        FriendsListViewModel flvm;
        public Friend Friend { get; set; }
        public FriendViewModel()
        {
            Friend = new Friend();
        }
        public FriendsListViewModel FriendsListViewModel
        {
            get { return flvm; }
            set
            {
                if (flvm == value) return;
                flvm = value;
                OnPropertyChanged("FriendsListViewModel");
            }
        }
        public string Name
        {
            get { return Friend.Name; }
            set
            {
                if (Friend.Name == value) return;
                Friend.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Email
        {
            get { return Friend.Email; }
            set
            {
                if (Friend.Email == value) return;
                Friend.Email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Phone
        {
            get { return Friend.Phone; }
            set
            {
                if (Friend.Phone == value) return;
                Friend.Phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public bool IsValid
        {
            get
            {
                return new string[] { Name, Email, Phone }.Any(x => !string.IsNullOrEmpty(x?.Trim()));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}