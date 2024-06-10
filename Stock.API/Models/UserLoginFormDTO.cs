using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stock.API.Models
{
    public class UserLoginFormDTO
    {
        [DisplayName("Identifiant")]
        [Required]
        public string Login { get; set; } = null!;

        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = null!;
    }
}
