using EcommerceApi.Models;

namespace EcommerceApi.DataRepos.Product_Repo
{
    public interface IProductRepo
    {
        public Task<List<ProductDTO>> GetAll(int? categoryId,int page, int size);
        public Task<List<ProductWithCategoryDTO>> GetWithCategory(int categoryId);
        public Task<ProductDTO> GetById(int id);

        public Task<ProductDTO> AddNew(ProductCreateDTO model);
        public Task<ProductDTO> UpdateOne(int id , ProductCreateDTO model);
        public Task<ProductDTO> DeleteOne(int id);

    }
}
