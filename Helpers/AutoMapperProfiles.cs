using AutoMapper;
using UdemyProject1.Helpers;
using UdemyProject1.Models.Domain;
using UdemyProject1.Models.DTO.Difficulty;
using UdemyProject1.Models.DTO.Region;
using UdemyProject1.Models.DTO.Walk;

namespace UdemyProject1.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<AddDifficultyDto, Difficulty>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}
