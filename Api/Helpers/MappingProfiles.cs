using Api.DTOs.CandidateDtos;
using Api.DTOs.CompanyDtos;
using Api.DTOs.JobDtos;
using Api.DTOs.UserDtos;
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

            CreateMap<PostJobDto, Job>();
            CreateMap<Job,JobToReturnForUser>()
                .ForMember(dest=>dest.Company,opt=>opt.MapFrom(src=>src.Company.Name));

            CreateMap<Job, JobToReturnForCompany>();

            CreateMap<Candidate, CandidateToReturnDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.GetUserName()))
                .ForMember(dest => dest.ResumeUrl, opt => opt.MapFrom(src => src.User.ResumeUrl))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Job.CompanyId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Job.Company.Name))
                .ForMember(dest => dest.JobLevel, opt => opt.MapFrom(src => src.Job.JobLevel))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title));





        }
    }
}
