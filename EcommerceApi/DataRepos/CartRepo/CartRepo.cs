using AutoMapper;
using EcommerceApi.Data;
using EcommerceApi.Data.Models;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.DataRepos.CartRepo
{
    public class CartRepo : ICartRepo
    {
        private readonly ApplicationDbContext _context;
        IMapper mapper = MapperProfile.CreateConfig();
        public CartRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateCart(CartCreateDTO cartModel)
        {
          Cart cart = mapper.Map<Cart>(cartModel);
          var addedCart = await _context.Carts.AddAsync(cart);
            if (addedCart != null)
            {
                _context.SaveChanges();
                return true;
            }
            else
            {

            return false;
            }
                 
        }

        public async Task<List<CartDTO>?> GetCartsById(int userId)
        {
            var carts = await _context.Carts.Where(c=> c.UserId == userId)
                .Join(_context.Products , c => c.ProductId , p => p.Id ,(cart , product)=> new CartDTO
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    ProductId = cart.ProductId,
                    Product = new ProductDTO
                    {
                        Id= product.Id,
                        Title = product.Title,
                        categoryId = product.CategoryId,
                        Description = product.Description,
                        Price = product.Price
                    },
                    Price = cart.Price,
                    Quantity = cart.Quantity,
                    totalPrice = cart.totalPrice
                })
                .ToListAsync();
            if(carts.Count == 0)
            {
                return null;
            }
            return carts;
        }

        public async Task<bool> RemoveAllItems(int userId)
        {
            var cartList = _context.Carts.Where(c => c.UserId == userId);
            if(cartList.Count() != 0)
            {
                _context.Carts.RemoveRange(cartList);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
           
        }

        public async Task<bool> RemoveCart(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if(cart == null) {
                return false;
            }
            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }

        public async Task<CartDTO?> UpdateCart(int cartId, CartCreateDTO cart)
        {
            var c = await  _context.Carts.FindAsync(cartId);
            if(c == null)
            {
                return null;
            }
            c.Price = cart.Price;
            c.Quantity = cart.Quantity;
            c.ProductId = cart.ProductId;
            c.UserId = cart.UserId;
            await _context.SaveChangesAsync();
            CartDTO reC = mapper.Map<CartDTO>(c);
            return reC;
        }
    }
}
