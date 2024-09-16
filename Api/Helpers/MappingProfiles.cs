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
        }
    }
}
