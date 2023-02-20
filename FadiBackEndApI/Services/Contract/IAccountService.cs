using Microsoft.AspNetCore.Identity;

namespace FadiBackEndApI.Services.Contract
{
    public interface IAccountService
    {
        Task<string> Login(string username, string password);

        //Task<string> Register(IdentityUser user);

        //Task<List<IdentityUser>> GetUsers();
    }
}
