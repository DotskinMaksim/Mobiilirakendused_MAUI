using System;
using AndroidX.ConstraintLayout.Helper.Widget;
using SQLite;

namespace MAUI_project.Models
{
	public class PlayerRepository
	{

        SQLiteConnection database;


        public PlayerRepository(string databasePath)
		{
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Players>();
        }
        public IEnumerable<Players> GetItems() =>

           database.Table<Players>().ToList();

        public Players GetItem(int id) =>

            database.Get<Players>(id);

        public int DeleteItem(int id) =>

            database.Delete<Players>(id);

        public int SaveItem(Players item)
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

