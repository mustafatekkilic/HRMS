using HRMS.Entities;
using HRMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<Users> _userManager;

        public RegisterController(UserManager<Users> userManager)
        {
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterViewModel userRegisterVM)
        {
            if (string.IsNullOrEmpty(userRegisterVM.Password) || string.IsNullOrEmpty(userRegisterVM.ConfirmPassword))
            {
                ModelState.AddModelError("", "Şifre boş geçilemez");
                return View();
            }

            if (!userRegisterVM.Password.Equals(userRegisterVM.ConfirmPassword))
            {
                ModelState.AddModelError("", "Şifreniz eşleşmiyor.");
                return View();
            }

            Users users = new Users
            {
                Name = userRegisterVM.Name,
                Surname = userRegisterVM.Surname,
                Email = userRegisterVM.EMail,
                UserName = userRegisterVM.UserName,
            };

            var result = await _userManager.CreateAsync(users, userRegisterVM.Password);

            if (result.Succeeded)
                return RedirectToAction("Index", "Login");
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
    }
}
