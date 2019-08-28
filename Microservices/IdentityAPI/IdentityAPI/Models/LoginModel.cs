using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.Models
{
    public class LoginModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email cannot be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
