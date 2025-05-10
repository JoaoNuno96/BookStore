using BookStore.Data;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.Models.Services
{
    public class LoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<bool> LoginAttempt(Auth authparam)
        {
            if (authparam == null)
            {
                return false;
            }

            var user = await this._userManager.FindByEmailAsync(authparam.Email);

            if(user == null)
            {
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, authparam.Password, false, false);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task LoggoutAttempt()
        {
            await this._signInManager.SignOutAsync();
        }

        public async Task<bool> AddUsers()
        {
            var userAdmin = new User
            {
                UserName = "joao",
                Email = "admin@test.pt"
            };

            var userGuest = new User
            {
                UserName = "guest",
                Email = "guest@test.pt"
            };

            await this._userManager.CreateAsync(userAdmin, "123456789Abc_");
            await this._userManager.CreateAsync(userGuest, "123456789Abc_");
            return true;
        }
    }
}
