using NZWalks.RESTful.Models.DTO.DifficultyModel;
using NZWalks.RESTful.Models.DTO.RegionModel;
using NZWalks.RESTful.Models.DTO.WalkCategoryModel;

namespace NZWalks.RESTful.Models.DTO.WalkModel
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string WalkImageUrl { get; set; }

        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }

        public ICollection<WalkCategoryDto> Categories { get; set; }
    }
}
