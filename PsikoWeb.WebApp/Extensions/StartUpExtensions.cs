using Microsoft.AspNetCore.Identity;
using PsikoWeb.WebApp.CustomValidations;
using PsikoWeb.WebApp.Localizations;
using Repository;

namespace PsikoWeb.WebApp.Extensions
{
    public static class StartUpExtensions
    {
        public static void AddIdentityWithExtensions(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcçdefgğhiıjklmnoöprsştuüvyzABCÇDEFGĞHİIJKLMNOÖPRSŞTUÜVYZ1234567890_.!";
                options.Password.RequiredLength = 6;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            })
            .AddPasswordValidator<PasswordValidator>()
            .AddUserValidator<UserValidator>()
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
            .AddEntityFrameworkStores<AppDbContext>(); 
        }
    }
}
