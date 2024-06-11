using MAUI_project.Models;
using MAUI_project.Views;

namespace MAUI_project
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "players.db";
        public static PlayerRepository database;
        public static PlayerRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new PlayerRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public App()
        {
            MainPage = new AppShell();
        }

    }
}