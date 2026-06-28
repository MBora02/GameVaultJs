using Microsoft.AspNetCore.Mvc;

namespace GameVaultJs.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminLogin()
        {
            return RedirectToAction("Index", "Game");
        }

        public IActionResult UserLogin()
        {
            return RedirectToAction("Index", "News");
        }
    }
}
