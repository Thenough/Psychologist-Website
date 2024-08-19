using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsikoWeb.WebApp.Modules;
using Repository;
using System.Diagnostics;

namespace PsikoWeb.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            return View(errorViewModel);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModule request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
          var identityResult = await  _userManager.CreateAsync(new() {UserName = request.UserName,PhoneNumber=request.PhoneNumber,Email=request.Email},request.PassWordConfirm);
            if (identityResult.Succeeded)
            {
               TempData["SuccessMessage"] = "Üyelik kayýt iþlemi baþarýlý bir þekilde gerçekleþmiþtir";
                return RedirectToAction(nameof(HomeController.SignUp));
            }
            foreach (IdentityError item in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty,item.Description);
            }
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> SignIn(SignInViewModule module,string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index","Home");

            var hasUser = await _userManager.FindByEmailAsync(module.Email);

            if(hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya þifre hatalý");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser,module.Password,module.RememberMe,true);

            if(signInResult.Succeeded)
            {
                return Redirect(returnUrl);
            }
            if(signInResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Çok fazla hatalý giriþ yaptýðýnýz. 3 dk sonra tekrar deneyiniz.");
                return View();
            }
            ModelState.AddModelError(string.Empty, "Giriþ baþarýsýz. Lütfen bilgilerinizi kontrol edin ve tekrar deneyin.");

            return View();
        }
    }
}
