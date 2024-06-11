using System;
using SQLite;

namespace MAUI_project.Models
{
    [Table("Players")]
    public class Players
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int VictoryCount{ get; set; }
        public DateTime LastWin { get; set; }

    }
}

