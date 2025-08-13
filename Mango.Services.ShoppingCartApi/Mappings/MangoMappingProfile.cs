using AutoMapper;
using Mango.Services.ShoppingCartApi.Models.DbModels;
using Mango.Services.ShoppingCartApi.Models.Dto;

namespace Mago.Services.ShoppingCartApi.Mappings;

public class MangoMappingProfile : Profile
{
    public MangoMappingProfile()
    {
        CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
        CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
    }

}
