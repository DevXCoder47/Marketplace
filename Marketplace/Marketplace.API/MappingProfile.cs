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
            CreateMap<Merchant, CreateMerchantDTO>().ReverseMap();
            CreateMap<UserModel, UserModelDTO>().ReverseMap();
            CreateMap<UserModel, UserLoginDTO>().ReverseMap();
            CreateMap<UserModel, UserSignUpDTO>().ReverseMap();
            CreateMap<UserModelDTO, UserSignUpDTO>().ReverseMap();
            CreateMap<UserModelDTO, UserLoginDTO>().ReverseMap();
            CreateMap<Company, CompanySignUpDTO>().ReverseMap();
            CreateMap<CompanyDTO, CompanySignUpDTO>().ReverseMap();
            CreateMap<CompanyDTO, CompanyLoginDTO>().ReverseMap();
        }
    }
}
