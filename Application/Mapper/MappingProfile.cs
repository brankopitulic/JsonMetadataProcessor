using Application.Commands.Create;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json.Linq;

namespace Application.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Command to Entity
        CreateMap<CreateClinicalTrialCommand, ClinicalTrial>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DurationInDays, opt => opt.Ignore());

        // Entity to Response DTO
        CreateMap<ClinicalTrial, ClinicalTrialResponse>();

        // JSON to Command
        CreateMap<JObject, CreateClinicalTrialCommand>()
            .ForMember(dest => dest.TrialId, opt => opt.MapFrom(src => MappingHelpers.ParseString(src, "trialId")))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => MappingHelpers.ParseString(src, "title")))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => MappingHelpers.ParseDateTime(src, "startDate")))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => MappingHelpers.ParseNullableDateTime(src, "endDate")))
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => MappingHelpers.ParseInt(src, "participants")))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MappingHelpers.ParseEnum<TrialStatus>(src, "status")));

        CreateMap<string, TrialStatus>()
            .ConvertUsing(status => MappingHelpers.ConvertToTrialStatus(status));

        CreateMap<TrialStatus, string>()
            .ConvertUsing(src => src.ToString());
    }
}
