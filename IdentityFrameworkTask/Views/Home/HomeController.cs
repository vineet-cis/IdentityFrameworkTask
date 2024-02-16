using IdentityFrameworkTask.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityFrameworkTask.Views.Home
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        public HomeController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Index()
        {
            string userName = userManager.GetUserName(User);
            return View("Index", userName);
        }
    }
}
