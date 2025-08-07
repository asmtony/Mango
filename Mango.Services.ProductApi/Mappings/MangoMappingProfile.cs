using AutoMapper;
using Mago.Services.ProductApi.Models.DbModels;
using Mago.Services.ProductApi.Models.Dto;

namespace Mago.Services.ProductAPI.Mappings;

public class MangoMappingProfile : Profile
{
    public MangoMappingProfile()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }

}
