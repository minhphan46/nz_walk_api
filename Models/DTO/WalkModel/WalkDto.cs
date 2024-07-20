using UdemyProject1.Models.Domain;
using UdemyProject1.Models.DTO.DifficultyModel;
using UdemyProject1.Models.DTO.RegionModel;
using UdemyProject1.Models.DTO.WalkTagModel;

namespace UdemyProject1.Models.DTO.WalkModel
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

        public ICollection<WalkTagDto> Tags { get; set; }
    }
}
