using System.ComponentModel.DataAnnotations;

namespace PsikoWeb.WebApp.Modules
{
    public class SignUpViewModule
    {
        public SignUpViewModule(string userName, string email, string phoneNumber, string passWord)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            PassWord = passWord;
        }
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }
        [Display(Name = "Email :")]
        public string Email { get; set; }
        [Display(Name = "Telefon :")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Şifre :")]
        public string PassWord { get; set; }
        [Display(Name = "Şifre Tekrar :")]
        public string PassWordConfirm { get; set; }
    }
}
