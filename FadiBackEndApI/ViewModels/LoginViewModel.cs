using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FadiBackEndApI.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
