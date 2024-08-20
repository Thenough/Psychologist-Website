using System.ComponentModel.DataAnnotations;

namespace PsikoWeb.WebApp.Modules
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "Email düzgün formatta girilmedi")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
