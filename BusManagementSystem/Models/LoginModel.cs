using System.ComponentModel.DataAnnotations;

namespace BusManagementSystem.Models
{
    public class LoginInfoModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
