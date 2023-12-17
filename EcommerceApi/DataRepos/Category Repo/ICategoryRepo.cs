using EcommerceApi.Data.Models;
using EcommerceApi.Models;

namespace EcommerceApi.DataRepos.Category_Repo
{
    public interface ICategoryRepo
    {
        public Task<List<CategoryDTO>> GetAll();
        public Task<CategoryDTO> GetById(int id);
        public Task<CategoryDTO> AddNew(CategoryCreateDTO category);
        public Task<CategoryDTO> UpdateById(int id, CategoryCreateDTO category);
        public Task<CategoryDTO> DeleteById(int id);
    }
}
