using AutoMapper;
using EcommerceApi.Data;
using EcommerceApi.Data.Models;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EcommerceApi.DataRepos.Product_Repo
{
    public class ProductRepo : IProductRepo
    {
        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;
        IMapper mapper = MapperProfile.CreateConfig();

        public async Task<ProductDTO> AddNew(ProductCreateDTO model)
        {
            Product product = mapper.Map<Product>(model);
            var prod = await _context.Products.AddAsync(product);
            _context.SaveChanges();
            ProductDTO productDTO = mapper.Map<ProductDTO>(product);
            return productDTO;
        }

        public async Task<ProductDTO?> DeleteOne(int id)
        {
            var deletedProd =await  _context.Products.FindAsync(id);
            if(deletedProd != null)
            {
                  _context.Products.Remove(deletedProd);
                  await _context.SaveChangesAsync();
                  ProductDTO dto = mapper.Map<ProductDTO>(deletedProd);
                  return dto;
            }
            return null;
        }

        public async Task<List<ProductDTO>> GetAll(int? categoryId = null , int page =1, int size = 10)
        {
            IEnumerable<Product> query =  _context.Products.Paginate(page, size);
            if (categoryId != null) {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            IEnumerable<Product> products = query.ToList();
            var prods = mapper.Map<IList<ProductDTO>>(products);

            return prods.ToList();

        }

        public async Task<ProductDTO> GetById(int id)
        {
             var product = await _context.Products
                .Where(prod => prod.Id == id)
                .SingleOrDefaultAsync();
            ProductDTO productDTO = mapper.Map<ProductDTO>(product);
            return productDTO;
        }

        public async Task<ProductDTO?> UpdateOne(int id , ProductCreateDTO model)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Title = model.Title;
                product.Description = model.Description;
                product.CategoryId = model.CategoryId; 
                   product.Price    = model.Price;
                await _context.SaveChangesAsync();

                ProductDTO productDTO = mapper.Map<ProductDTO>(product);
                return productDTO;
            }
            return null;
        }

        public async Task<List<ProductWithCategoryDTO>> GetWithCategory(int categoryId)
        {
       
                var cc = await _context.Products.Where(p => p.CategoryId == categoryId)
                .Join(_context.Categories, (p) => p.CategoryId, (c) => c.Id, (p, c) => new ProductWithCategoryDTO()
                {
                    Id = p.Id,
                    CategoryId = c.Id,
                    Description = p.Description,
                    Title = p.Title,
                    CategoryName = c.Name,
                    Price = p.Price
                }).ToListAsync();

                return cc;
            
        }
    }
}
