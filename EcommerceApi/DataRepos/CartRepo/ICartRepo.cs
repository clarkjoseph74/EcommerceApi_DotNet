using EcommerceApi.Models;

namespace EcommerceApi.DataRepos.CartRepo
{
    public interface ICartRepo
    {
        public Task<List<CartDTO>> GetCartsById(int userId);
        public Task<bool> CreateCart(CartCreateDTO cartModel);
        public Task<bool> RemoveCart(int cartId);
        public Task<bool> RemoveAllItems(int userId);
        public Task<CartDTO> UpdateCart(int cartId, CartCreateDTO cart);
    }
}
