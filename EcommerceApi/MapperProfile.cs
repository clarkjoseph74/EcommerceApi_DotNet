using AutoMapper;
using EcommerceApi.Data.Models;
using EcommerceApi.Models;

namespace EcommerceApi
{
    public static class MapperProfile
    {
        public static IMapper CreateConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>().ReverseMap();
                cfg.CreateMap<Product, ProductCreateDTO>().ReverseMap();
                cfg.CreateMap<Cart, CartCreateDTO>().ReverseMap();
                cfg.CreateMap<Cart, CartDTO>().ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}
