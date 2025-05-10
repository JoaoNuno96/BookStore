using BookStore.Models.Entities;
using BookStore.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginParam) 
        {
            this._loginService = loginParam;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnterAuth(Auth authParam)
        {
            bool status = await this._loginService.LoginAttempt(authParam);

            if (status)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this._loginService.LoggoutAttempt();
            return RedirectToAction("Index", "Home");
        }

    }
}
