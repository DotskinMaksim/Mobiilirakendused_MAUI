using SQLite;

namespace MAUI_project.Models
{
    public class FriendRepository
    {
        SQLiteConnection database;
        public FriendRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Friend>();
        }
        public IEnumerable<Friend> GetItems() =>
            database.Table<Friend>().ToList();
        public Friend GetItem(int id) =>
            database.Get<Friend>(id);
        public int DeleteItem(int id) =>
            database.Delete<Friend>(id);
        public int SaveItem(Friend item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            return database.Insert(item);
        }
    }
}