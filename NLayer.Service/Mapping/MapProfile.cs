using AutoMapper;
using NLayer.Core.Dtos;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
            //Productı productwithcategoryDto ya dönüştürüyoruz.
            CreateMap<Product, ProductsWithCategoryDto>().ReverseMap();
            //Categoriyi CategoryWithProductsDto ya çeviriyoruz.
            CreateMap<Category, CategoryWithProductsDto>();
        }
    }
}
