using System.ComponentModel.DataAnnotations;

namespace PsikoWeb.WebApp.Areas.Admin.Models
{
    public class RoleAddViewModel
    {
        [Required(ErrorMessage ="Rol isim alanı boş bırakılamaz")]
        [Display(Name ="Rol ismi")]
        public string Name { get; set; }
    }
}
