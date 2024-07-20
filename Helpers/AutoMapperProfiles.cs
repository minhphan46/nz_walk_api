using AutoMapper;
using UdemyProject1.Helpers;
using UdemyProject1.Models.Domain;
using UdemyProject1.Models.DTO.DifficultyModel;
using UdemyProject1.Models.DTO.RegionModel;
using UdemyProject1.Models.DTO.TagModel;
using UdemyProject1.Models.DTO.WalkModel;
using UdemyProject1.Models.DTO.WalkTagModel;

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
            CreateMap<Walk, WalkDto>()
                .ForMember(x => x.Tags, opt => opt.MapFrom(x => x.WalkTags))
                .ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<AddDifficultyDto, Difficulty>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<AddTagDto, Tag>().ReverseMap();
            CreateMap<WalkTag, WalkTagDto>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag))
                .ReverseMap();
        }
    }
}
