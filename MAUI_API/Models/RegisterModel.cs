using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MAUI_API.Models
{
    [Keyless]
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
