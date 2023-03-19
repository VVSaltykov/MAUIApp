using MAUI_API.Interfaces;

namespace MAUI_API.Models
{
    public class Home: IEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
