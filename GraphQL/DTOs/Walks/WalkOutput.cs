using NZWalks.Entities;
using NZWalks.GraphQL.DataLoaders;

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

        //public ICollection<WalkCategoryDto> Categories { get; set; }
        public async Task<ICollection<WalkCategory>> Categories([Service] WalkCategorieDataLoader dataLoader)
        {
            ICollection<WalkCategory> walkCategories = await dataLoader.LoadAsync(Id, CancellationToken.None);
            return walkCategories;
        }
    }
}
