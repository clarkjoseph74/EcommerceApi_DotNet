using EcommerceApi.Data;
using EcommerceApi.Data.Models;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.DataRepos.Category_Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        public  CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public async Task<List<CategoryDTO>> GetAll()
        {
            return await _context.Categories.Select(cat => new CategoryDTO { Id = cat.Id , Name = cat.Name}).ToListAsync();
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            return await _context.Categories
                .Select(cat => new CategoryDTO { Id = cat.Id, Name = cat.Name })
                .Where(category => category.Id == id)
                .SingleOrDefaultAsync();
        }
        public async Task<CategoryDTO>  AddNew(CategoryCreateDTO category)
        {
            Category cat = new Category()
            {
                Name = category.Name,
            };
            await _context.Categories.AddAsync(cat);
            _context.SaveChanges();
            CategoryDTO reCat = new CategoryDTO() { Id = cat.Id , Name = cat.Name };
            return reCat;
        }

        public async Task<CategoryDTO?> UpdateById(int id, CategoryCreateDTO category)
        {
          
             var cat = await _context.Categories.FindAsync(id);
             cat.Name = category.Name;
             _context.SaveChanges();
            if (cat != null)
            {
               CategoryDTO reCat = new CategoryDTO() { 
                            Id = cat.Id,
                            Name = cat.Name,
                        }; 
                return reCat;
            }

            return null;
           
        }

        public async Task<CategoryDTO?> DeleteById(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if(cat != null)
            {
                _context.Categories.Remove(cat);
                _context.SaveChanges();
                CategoryDTO reCat = new CategoryDTO()
                {
                    Id = cat.Id,
                    Name = cat.Name,
                };
                return reCat;
            }
            return null;
        }
    }
}
