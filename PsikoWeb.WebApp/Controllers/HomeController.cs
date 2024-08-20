using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsikoWeb.WebApp.Modules;
using PsikoWeb.WebApp.Services;
using Repository;
using System.Collections.Generic;
using System.Diagnostics;

namespace PsikoWeb.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> ForgetPassword(ForgetPasswordViewModule request)
        {
            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser == null) 
            {
                ModelState.AddModelError(string.Empty,"Bu e-mail adresine sahip kullanýcý bulunamamýþtýr");
                return View();
            }

            string passwordResetToken =await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            var passwordResetLink = Url.Action("ResetPassword","Home",new
            {
                userId = hasUser.Id,
                Token = passwordResetToken
            },HttpContext.Request.Scheme,"localhost:7238");
            // https://localost:7238?userId=1213123&token=askdjhsajhgdquweq
            await _emailService.SendResetPasswordEmail(passwordResetLink,hasUser.Email);

            TempData["SuccessMessage"] = "Þifre yenileme linki e posta adresinize gönderilmiþtir";
            return RedirectToAction(nameof(ForgetPassword));
        }
        public  IActionResult ResetPassword(string userId,string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;   
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModule request)
        {
            string userId = TempData["userId"]!.ToString();
            string token = TempData["token"]!.ToString();
            var hasUser = await _userManager.FindByIdAsync(userId!);
            if (userId == null || token == null)
            {
                throw new Exception("Bir hata meydana geldi");
            }
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanýcý bulunamamýþtýr");
                return View();
            }
            IdentityResult result = await _userManager.ResetPasswordAsync(hasUser, token!, request.PassWord);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Þifreniz baþarýyla yenilenmiþtir";
            }
            else
            {
                ModelState.AddModelError(string.Empty,"Þifrenizi deðiþtirirken hata oluþtu");
               
            }
            return View();
        }
    }
}
