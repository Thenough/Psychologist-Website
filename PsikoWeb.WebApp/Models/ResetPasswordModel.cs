using System.ComponentModel.DataAnnotations;

namespace PsikoWeb.WebApp.Modules
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        [Display(Name = "Yeni Şifre :")]
        public string PassWord { get; set; }

        [Compare(nameof(PassWord), ErrorMessage = "Şifre aynı değil")]
        [Required(ErrorMessage = "Lütfen şifre tekrarını giriniz")]
        [Display(Name = "Yeni Şifre Tekrar :")]
        public string PassWordConfirm { get; set; }
    }
}
