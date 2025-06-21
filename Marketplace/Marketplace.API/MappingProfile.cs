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
            CreateMap<Company, CompanySignUpDTO>().ReverseMap();
            CreateMap<CompanyDTO, CompanySignUpDTO>().ReverseMap();
            CreateMap<CompanyDTO, CompanyLoginDTO>().ReverseMap();
        }
    }
}
