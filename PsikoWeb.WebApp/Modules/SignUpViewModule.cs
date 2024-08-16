using System.ComponentModel.DataAnnotations;

namespace PsikoWeb.WebApp.Modules
{
    public class SignUpViewModule
    {
        public SignUpViewModule()
        {
            
        }
        public SignUpViewModule(string userName, string email, string phoneNumber, string passWord)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            PassWord = passWord;
        }
        [Required(ErrorMessage ="Lütfen Kullanıcı Adı giriniz")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Lütfen Email adresini uygun formatta giriniz")]
        [Required(ErrorMessage = "Lütfen Email giriniz")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen telefon numarası giriniz")]
        [Display(Name = "Telefon :")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        [Display(Name = "Şifre :")]
        public string PassWord { get; set; }

        [Compare(nameof(PassWord),ErrorMessage = "Şifre aynı değil")]
        [Required(ErrorMessage = "Lütfen şifre tekrarını giriniz")]
        [Display(Name = "Şifre Tekrar :")]
        public string PassWordConfirm { get; set; }
    }
}
