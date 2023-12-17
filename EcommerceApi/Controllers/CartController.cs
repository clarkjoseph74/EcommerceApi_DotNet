using EcommerceApi.DataRepos.CartRepo;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _repo;

        public CartController(ICartRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllOfUser(int userId)
        {
            var carts = await _repo.GetCartsById(userId);
            if(carts == null)
            {
                return NotFound($"No carts for this user with id: {userId}");
            }
            return Ok(carts);
        }
        [HttpPost("Items")]
        public async Task<IActionResult> CreateCart(CartCreateDTO cartModel)
        {
            if(!ModelState.IsValid)
            {
             return BadRequest("All Fields is Required");
  
            }
            else
            {
                var cart = await _repo.CreateCart(cartModel);
                if(cart == true)
                {
                    return Ok("The item is added Successfully");
                }
                return BadRequest("There is Problem while adding item!");
            }

        }
        [HttpDelete("Single/{cartId}")]
        public async Task<IActionResult> RemoveItem(int cartId)
        {
            var isSuccuess = await _repo.RemoveCart(cartId);
            if (isSuccuess)
            {
                return Ok("The cart is Deleted successfully");
            }
            return BadRequest("There is error while deleting the cart. try again");

        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveAllItemsOfUser(int userId)
        {
            var isSuccuess = await _repo.RemoveAllItems(userId);
            if (isSuccuess)
            {
                return Ok("The cart is Deleted successfully");
            }
            return BadRequest("There is error while deleting the cart. try again");

        }

        [HttpPut("{cartId}")]
        public async Task<IActionResult> UpdateCart(int cartId , CartCreateDTO cart)
        {
            var item = await _repo.UpdateCart(cartId , cart);
            if (item != null)
            {
                return Ok(item);
            }
            return BadRequest("There is error while updating the cart. try again");

        }
    }
}
