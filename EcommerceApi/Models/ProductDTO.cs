namespace EcommerceApi.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int categoryId { get; set; }
        public decimal Price { get; set; }
    }
}
