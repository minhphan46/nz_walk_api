using AutoMapper;
using UdemyProject1.Models.Domain;
using UdemyProject1.Models.DTO.DifficultyModel;
using UdemyProject1.Models.DTO.RegionModel;
using UdemyProject1.Models.DTO.CategoryModel;
using UdemyProject1.Models.DTO.WalkModel;
using UdemyProject1.Models.DTO.WalkCategoryModel;

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
                .ForMember(x => x.Categories, opt => opt.MapFrom(x => x.WalkCategories))
                .ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<AddDifficultyDto, Difficulty>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryDto, Category>().ReverseMap();
            CreateMap<WalkCategory, WalkCategoryDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();
        }
    }
}
