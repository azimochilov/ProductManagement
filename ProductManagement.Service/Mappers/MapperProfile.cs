using AutoMapper;
using ProductManagement.Domain.Entities;
using ProductManagement.Service.DTOs.Products;

namespace ProductManagement.Service.Mappers;
public class MapperProfile : Profile
{
    public MapperProfile()
    {

    CreateMap<Product, ProductForCreationDto>().ReverseMap();
    CreateMap<Product, ProductForResultDto>().ReverseMap();
    
    }

}
