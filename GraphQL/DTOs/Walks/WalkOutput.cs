using NZWalks.Entities;
using NZWalks.GraphQL.DataLoaders;
using NZWalks.RESTful.Models.DTO.DifficultyModel;
using NZWalks.RESTful.Models.DTO.RegionModel;
using NZWalks.RESTful.Models.DTO.WalkCategoryModel;

namespace NZWalks.GraphQL.DTOs.Walks
{
    public class WalkOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string WalkImageUrl { get; set; }

        [GraphQLIgnore]
        public Guid DifficultyId { get; set; }

        [GraphQLIgnore]
        public Guid RegionId { get; set; }

        public async Task<Difficulty> Difficulty([Service] DifficultyDataLoader dataLoader)
        {
            Difficulty difficulty = await dataLoader.LoadAsync(DifficultyId, CancellationToken.None);

            return difficulty;
        }

        public async Task<Region> Region([Service] RegionDataLoader dataLoader)
        {
            Region region = await dataLoader.LoadAsync(RegionId, CancellationToken.None);

            return region;
        }

        public ICollection<WalkCategoryDto> Categories { get; set; }
    }
}
