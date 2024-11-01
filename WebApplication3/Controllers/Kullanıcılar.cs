using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Areas.Identity.Data;

namespace WebApplication3.Controllers
{
    public class Kullanıcılar : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public Kullanıcılar(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users  = _userManager.Users.ToList();
            return View(users);
        }
    }
}
