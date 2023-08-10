using IdentitySonProje.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IdentitySonProje.Areas.User.Controllers
{
    [Area("User")]
    [Authorize("User")]
    public class PanelController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public PanelController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult AddCart(int id)
        {
            return View();
        }
        
        private int GetUserId()
        {
            return int.Parse(userManager.GetUserId(User));
        }

    }
}
