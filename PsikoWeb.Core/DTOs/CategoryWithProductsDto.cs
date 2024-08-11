namespace Core.DTOs
{
    public class CategoryWithProductsDto : CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();

    }
}
