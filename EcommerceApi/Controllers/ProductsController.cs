using EcommerceApi.DataRepos.Product_Repo;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _repo;

        public ProductsController(IProductRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int? categoryId , [FromQuery] int page , [FromQuery] int size)
        {
            return Ok(await _repo.GetAll(categoryId , page , size));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _repo.GetById(id);
            if(product == null)
            {
                return NotFound($"Product with ID {id} not found !");
            }
            return Ok(product);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetWithCategory( int categoryId)
        {
            var products = await _repo.GetWithCategory(categoryId);
          
            return Ok(products );
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductCreateDTO model)
        {
            var product = await _repo.AddNew(model);
            return Ok(product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id , [FromBody] ProductCreateDTO model)
        {
            var product = await _repo.UpdateOne(id, model);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found !");
            }
            return Ok(product);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repo.DeleteOne(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found !");
            }
            return Ok(product);
        }

    }
}
