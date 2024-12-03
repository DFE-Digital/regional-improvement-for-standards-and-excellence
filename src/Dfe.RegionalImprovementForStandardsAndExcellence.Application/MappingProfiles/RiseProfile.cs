using AutoMapper;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.Common.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Application.SupportProject.Models;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.Entities.SupportProject;
using Dfe.RegionalImprovementForStandardsAndExcellence.Domain.ValueObjects;

namespace Dfe.RegionalImprovementForStandardsAndExcellence.Application.MappingProfiles
{
    public class RiseProfile : Profile
    {
        public RiseProfile()
        {
            // Explicit mapping from SupportProjectId to int and vice versa
            CreateMap<SupportProjectId, int>()
                .ConvertUsing(src => src.Value);

            CreateMap<int, SupportProjectId>()
                .ConvertUsing(value => new SupportProjectId(value));

            CreateMap<Domain.Entities.SupportProject.SupportProject, SupportProjectDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id.Value))
                .ReverseMap();
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PrincipalId.Value))
                //.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.NameDetails.NameListAs!.Split(",", StringSplitOptions.None)[1].Trim()))
                //.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.NameDetails.NameListAs!.Split(",", StringSplitOptions.None)[0].Trim()))
                //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => 6src.PrincipalDetails.Email))
                //.ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NameDetails.NameDisplayAs))
                //.ForMember(dest => dest.DisplayNameWithTitle, opt => opt.MapFrom(src => src.NameDetails.NameFullTitle))
                //.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => new List<string> { "Student" }))
                //.ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.LastRefresh))
                //.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PrincipalDetails.Phone))
                //.ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.SchoolName));
        }
    }
}
