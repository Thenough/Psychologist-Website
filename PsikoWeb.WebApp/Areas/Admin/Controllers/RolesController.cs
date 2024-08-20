using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PsikoWeb.WebApp.Areas.Admin.Models;
using Repository;

namespace PsikoWeb.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleAdd(RoleAddViewModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole { Name = request.Name});
            if (!result.Succeeded) 
            {
                ModelState.AddModelError(string.Empty,"Rol Eklenemedi");
                return View();
            }
            return RedirectToAction(nameof(RolesController.Index));
        }

    }
}
