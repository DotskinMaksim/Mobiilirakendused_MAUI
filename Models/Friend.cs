using SQLite;
namespace MAUI_project.Models
{
    [Table("Friends")]

    public class Friend
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}

