using EcommerceApi.Models;
using EcommerceApi.DataRepos.Category_Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _repo;

        public CategoriesController(ICategoryRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _repo.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            CategoryDTO category = await _repo.GetById(id);
            if (category == null)
            {
                return NotFound($"Item with ID : {id} Not Found");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateDTO model)
        {
            CategoryDTO category = await _repo.AddNew(model);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryCreateDTO model)
        {
            CategoryDTO category = await _repo.UpdateById(id, model);
            if (category == null)
            {
                return NotFound($"The Category with Id {id} not found");
            }
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            CategoryDTO category = await _repo.DeleteById(id);
            if (category == null)
            {
                return NotFound($"The Category with Id {id} not found");
            }
            return Ok(category);
        }
    }
}
