﻿using Microsoft.AspNetCore.Identity;
using Repository;

namespace PsikoWeb.WebApp.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if(password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new() { Code = "PasswordContainsUserName",Description="Şifre, kullanıcı adı içeremez"});
            }
            if (password!.ToLower().StartsWith("1234"))
            {
                errors.Add(new() { Code = "PasswordContains1234", Description = "Şifre, ardışık sayı içeremez" });
            }
            if(errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
