using AutoMapper;
using NZWalks.Entities;
using NZWalks.GraphQL.DTOs.Difficulties;
using NZWalks.GraphQL.Resolvers;

namespace NZWalks.GraphQL.Schema.Queries
{
    [ExtendObjectType("Query")]
    public class DifficultyQuery
    {
        private readonly DifficultiesResolver _resolver;
        private readonly IMapper mapper;

        public DifficultyQuery(DifficultiesResolver difficultiesResolver, IMapper mapper)
        {
            _resolver = difficultiesResolver;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DifficultyOutput>> GetDifficulties()
        {
            var difficulties = await _resolver.GetAllAsync();
            return mapper.Map<IEnumerable<DifficultyOutput>>(difficulties);
        }

        public async Task<DifficultyOutput> GetDifficulty(Guid id)
        {
            var difficulty = await _resolver.GetByIdAsync(id);

            if (difficulty == null)
            {
                throw new GraphQLException(new Error("Difficulty not found.", "DIFFICULTY_NOT_FOUND"));
            }
            return mapper.Map<DifficultyOutput>(difficulty);
        }
    }
}
