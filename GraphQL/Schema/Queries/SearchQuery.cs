using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data.DbContexts;
using NZWalks.GraphQL.DTOs.Base;
using NZWalks.GraphQL.DTOs.Categories;
using NZWalks.GraphQL.DTOs.Difficulties;
using NZWalks.GraphQL.DTOs.Regions;
using NZWalks.GraphQL.DTOs.Walks;
using NZWalks.GraphQL.Resolvers;

namespace NZWalks.GraphQL.Schema.Queries
{
    [ExtendObjectType("Query")]
    public class SearchQuery
    {
        private readonly WalksResolver _resolver;
        private readonly IMapper mapper;

        public SearchQuery(WalksResolver walksResolver, IMapper mapper)
        {
            _resolver = walksResolver;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<IBaseOutput>> Search(string text, [Service] NZWalksDbContext context)
        {
            IEnumerable<WalkOutput> walks = await context.Walks
                        .Where(w => w.Name == text)
                        .Select(w => new WalkOutput()
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Description = w.Description,
                            DifficultyId = w.DifficultyId,
                            LengthInKm = w.LengthInKm,
                            RegionId = w.RegionId,
                            WalkImageUrl = w.WalkImageUrl
                        }
                   ).ToListAsync();

            IEnumerable<CategoryOutput> categories = await context.Categories
                        .Where(w => w.Name == text)
                        .Select(w => new CategoryOutput()
                        {
                            Id = w.Id,
                            Name = w.Name
                        }
                   ).ToListAsync();

            IEnumerable<DifficultyOutput> difficulties = await context.Difficulties
                        .Where(w => w.Name == text)
                        .Select(w => new DifficultyOutput()
                        {
                            Id = w.Id,
                            Name = w.Name
                        }
                   ).ToListAsync();

            IEnumerable<RegionOutput> regions = await context.Regions
                .Where(w => w.Name == text)
                        .Select(w => new RegionOutput()
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Code = w.Code,
                            RegionImageUrl = w.RegionImageUrl
                        }
                   ).ToListAsync();

            return new List<IBaseOutput>()
                        .Concat(walks)
                        .Concat(categories)
                        .Concat(difficulties)
                        .Concat(regions);
        }
    }
}
