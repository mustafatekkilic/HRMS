using HRMS.Entities;
using HRMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<Users> _signInManager;

        public LoginController(SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLoginViewModel userLoginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.Username, userLoginViewModel.Password, true, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ModelState.AddModelError("Username", "Hatalı kullanıcı adı veya şifre");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
