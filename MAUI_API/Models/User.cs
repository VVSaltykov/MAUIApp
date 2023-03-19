using MAUI_API.Definitions;

namespace MAUI_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; } = Role.USER;
    }
}
