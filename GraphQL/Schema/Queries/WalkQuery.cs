using AutoMapper;
using NZWalks.Data.DbContexts;
using NZWalks.GraphQL.DTOs.Walks;
using NZWalks.GraphQL.Resolvers;
using NZWalks.GraphQL.Schema.Filters;

namespace NZWalks.GraphQL.Schema.Queries
{
    [ExtendObjectType("Query")]
    public class WalkQuery
    {
        private readonly WalksResolver _resolver;
        private readonly IMapper mapper;

        public WalkQuery(WalksResolver walksResolver, IMapper mapper)
        {
            _resolver = walksResolver;
            this.mapper = mapper;
        }

        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseFiltering(typeof(WalkFilterType))]
        public async Task<IEnumerable<WalkOutput>> GetWalks()
        {
            var walks = await _resolver.GetAllAsync();
            return mapper.Map<IEnumerable<WalkOutput>>(walks);
        }

        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseFiltering(typeof(WalkFilterType))]
        public IQueryable<WalkOutput> GetWalksDb([Service] NZWalksDbContext context)
        {
            return context.Walks.Select(
                        w => new WalkOutput()
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Description = w.Description,
                            DifficultyId = w.DifficultyId,
                            LengthInKm = w.LengthInKm,
                            RegionId = w.RegionId,
                            WalkImageUrl = w.WalkImageUrl
                        }
                    );
        }

        public async Task<WalkOutput> GetWalk(Guid id)
        {
            var walk = await _resolver.GetByIdAsync(id);

            if (walk == null)
            {
                throw new GraphQLException(new Error("Walk not found.", "WALK_NOT_FOUND"));
            }
            return mapper.Map<WalkOutput>(walk);
        }
    }
}
