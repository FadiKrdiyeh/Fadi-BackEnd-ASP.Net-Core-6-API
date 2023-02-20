using Microsoft.AspNetCore.Identity;

namespace FadiBackEndApI.Models
{
    public class FadiAppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
