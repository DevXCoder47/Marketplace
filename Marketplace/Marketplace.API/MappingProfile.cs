using AutoMapper;
using Marketplace.Core.DTOs;
using Marketplace.Core.Models;

namespace Marketplace.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<ImageDTO, AddImageDTO>().ReverseMap();
            CreateMap<Company, CompanySignUpDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<CompanyDTO, CompanySignUpDTO>().ReverseMap();
            CreateMap<CompanyDTO, CompanyLoginDTO>().ReverseMap();
        }
    }
}
