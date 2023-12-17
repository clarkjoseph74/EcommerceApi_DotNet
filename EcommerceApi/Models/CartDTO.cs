namespace EcommerceApi.Models
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId {  get; set; }
        public decimal Price { get; set; }
        public decimal totalPrice { get; set; }
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }
    }
}
