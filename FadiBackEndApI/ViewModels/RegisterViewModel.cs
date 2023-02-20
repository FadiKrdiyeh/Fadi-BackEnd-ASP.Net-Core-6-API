using System.ComponentModel.DataAnnotations;

namespace FadiBackEndApI.ViewModels
{
    public class RegisterViewModel
    {
        //[Required]
        public string Username { get; set; }

        //[Required]
        public string Password { get; set; }

        //[Required]
        public string FullName { get; set; }

        public string Token { get; set; }
    }
}
