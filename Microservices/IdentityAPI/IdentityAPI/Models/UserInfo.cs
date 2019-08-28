using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityAPI.Models
{
    [Table("Users")]
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name of event cannot be empty")]
        public string FirstName { get; set; }

        
        [Required(ErrorMessage = "Last Name cannot be empty")]        
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email cannot be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

    }
}
