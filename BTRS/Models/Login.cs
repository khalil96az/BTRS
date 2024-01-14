using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please fill the username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please fill the password")]
        public string password { get; set; }
    }
}
