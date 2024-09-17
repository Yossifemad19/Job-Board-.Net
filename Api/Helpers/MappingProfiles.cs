using Api.DTOs;
using AutoMapper;
using Core.Entities;

namespace Api.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            //Company maps
            CreateMap<CompanyDto,Company>()
                .ForMember(x=>x.PasswordHash,opt=>opt.Ignore());
            CreateMap<Company, CompanyToReturn>();
            CreateMap<Company, GetCompanyDto>();  

            CreateMap<UserResiterDto,AppUser>()
                .ForMember(x => x.PasswordHash, opt => opt.Ignore());

            CreateMap<AppUser, UserToReturn>();

        }
    }
}
