using Core.Models.Abstarct;

namespace Core.Models.Concrete
{
    public class Admin : BaseEntity
    {
        public string Username { get; set; }
        public string PassWord { get; set; }

    }
}
