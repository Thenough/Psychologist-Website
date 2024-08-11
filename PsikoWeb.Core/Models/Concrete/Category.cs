using Core.Models.Abstarct;

namespace Core.Models.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
