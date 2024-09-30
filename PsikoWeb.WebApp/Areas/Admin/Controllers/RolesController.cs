using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
           var roles = await _roleManager.Roles.Select(x =>new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name!
            }).ToListAsync();
            return View(roles);
        }

        [Authorize(Roles ="admin")]
        public IActionResult RoleAdd()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RoleAdd(RoleAddViewModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole { Name = request.Name.ToLower()});
            if (!result.Succeeded) 
            {
                ModelState.AddModelError(string.Empty,"Rol Eklenemedi");
                return View();
            }
            TempData["SuccessMessage"] = "Rol başarılı şekilde eklendi";
            return RedirectToAction(nameof(RolesController.Index));
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> RoleUpdate(string id)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id);
            if(roleToUpdate == null)
            {
                throw new Exception("Güncellenecek rol bulunamamıştır");
            }
            return View(new RoleUpdateViewModel() { Id = roleToUpdate.Id,Name = roleToUpdate.Name});
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> RoleUpdate(RoleUpdateViewModel request)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id);
            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek rol bulunamamıştır");
            }
            roleToUpdate.Name = request.Name;
            await _roleManager.UpdateAsync(roleToUpdate);
            ViewData["SuccessMessage"] = "Rol bilgisi güncellendi";
            return View();
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> RoleDelete(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);
            if (roleToDelete == null)
            {
                throw new Exception("Silinecek rol bulunamamıştır");
            }
            var result = await _roleManager.DeleteAsync(roleToDelete);
            if(!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x =>x.Description).First());
            }
            TempData["SuccessMessage"] = "Rol başarılı şekilde silinmiştir";
            return RedirectToAction(nameof(RolesController.Index));
        }
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AssignRoleUser(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);
            ViewBag.userId = id;
            var roles =await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var roleViewModel = new List<AssignRoleToUserViewModel>();
            
            foreach (var item in roles)
            {
                var assignRoleToUserViewModel = new AssignRoleToUserViewModel() { Id = item.Id,Name = item.Name!};
                if(userRoles.Contains(item.Name!))
                {
                    assignRoleToUserViewModel.Exist = true;
                }
                roleViewModel.Add(assignRoleToUserViewModel);
            }
            return View(roleViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> AssignRoleUser(string userId,List<AssignRoleToUserViewModel> requestList)
        {
            var userToAssignRoles = await _userManager.FindByIdAsync(userId);
            foreach (var item in requestList)
            {
                if(item.Exist)
                {
                  await  _userManager.AddToRoleAsync(userToAssignRoles,item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToAssignRoles, item.Name);
                }
            }
            return RedirectToAction(nameof(HomeController.UserList),"Home");
        }
    }
}
