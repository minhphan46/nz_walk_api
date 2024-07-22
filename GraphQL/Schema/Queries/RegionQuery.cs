using AutoMapper;
using NZWalks.GraphQL.DTOs.Regions;
using NZWalks.GraphQL.DTOs.Regions;
using NZWalks.GraphQL.Resolvers;

namespace NZWalks.GraphQL.Schema.Queries
{
    [ExtendObjectType("Query")]
    public class RegionQuery
    {
        private readonly RegionsResolver _resolver;
        private readonly IMapper mapper;

        public RegionQuery(RegionsResolver regionsResolver, IMapper mapper)
        {
            _resolver = regionsResolver;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RegionOutput>> Regions()
        {
            var regions = await _resolver.GetAllAsync();
            return mapper.Map<IEnumerable<RegionOutput>>(regions);
        }

        public async Task<RegionOutput> Region(Guid id)
        {
            var region = await _resolver.GetByIdAsync(id);

            if (region == null)
            {
                throw new GraphQLException(new Error("Region not found.", "REGION_NOT_FOUND"));
            }
            return mapper.Map<RegionOutput>(region);
        }
    }
}
