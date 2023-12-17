namespace EcommerceApi.Models
{
    public class ProductWithCategoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set;}
        public string CategoryName { get; set; }
        public decimal Price {  get; set; }
    }
}
