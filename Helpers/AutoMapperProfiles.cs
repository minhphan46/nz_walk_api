using AutoMapper;
using NZWalks.Entities;
using NZWalks.GraphQL.Schema.Mutations;
using NZWalks.RESTful.Models.DTO.CategoryModel;
using NZWalks.RESTful.Models.DTO.DifficultyModel;
using NZWalks.RESTful.Models.DTO.RegionModel;
using NZWalks.RESTful.Models.DTO.WalkCategoryModel;
using NZWalks.RESTful.Models.DTO.WalkModel;

namespace NZWalks.Helpers
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

            // GraphQl
            CreateMap<Category, CategoryInput>().ReverseMap();
            CreateMap<Category, CategoryOutput>().ReverseMap();
        }
    }
}
