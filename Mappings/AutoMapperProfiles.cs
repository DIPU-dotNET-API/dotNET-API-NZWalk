using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegionDTO, Region>().ReverseMap();
        CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
        CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
    }
}