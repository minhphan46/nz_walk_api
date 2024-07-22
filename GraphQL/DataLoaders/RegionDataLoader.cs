using NZWalks.Entities;
using NZWalks.GraphQL.Resolvers;

namespace NZWalks.GraphQL.DataLoaders
{
    public class RegionDataLoader : BatchDataLoader<Guid, Region>
    {
        private readonly RegionsResolver _resolver;

        public RegionDataLoader(RegionsResolver regionsResolver, IBatchScheduler batchScheduler, DataLoaderOptions options) : base(batchScheduler, options)
        {
            _resolver = regionsResolver;
        }

        protected override async Task<IReadOnlyDictionary<Guid, Region>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            IEnumerable<Region> regions = await _resolver.GetRegionsByWalkId(keys);

            return regions.ToDictionary(i => i.Id);
        }
    }
}
