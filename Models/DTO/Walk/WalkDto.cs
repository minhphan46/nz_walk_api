using UdemyProject1.Models.Domain;
using UdemyProject1.Models.DTO.Difficulty;
using UdemyProject1.Models.DTO.Region;

namespace UdemyProject1.Models.DTO.Walk
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

        public ICollection<WalkTag> WalkTags { get; set; }
    }
}
