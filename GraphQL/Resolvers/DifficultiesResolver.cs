using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NZWalks.Data.DbContexts;
using NZWalks.Entities;

namespace NZWalks.GraphQL.Resolvers
{
    public class DifficultiesResolver
    {
        private readonly IDbContextFactory<NZWalksDbContext> _dbContextFactory;

        public DifficultiesResolver(IDbContextFactory<NZWalksDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                await context.Difficulties.AddAsync(difficulty);
                await context.SaveChangesAsync();
                return difficulty;
            }
        }

        public async Task<Difficulty> DeleteAsync(Guid id)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                var existingDifficulty = await context.Difficulties.FirstOrDefaultAsync(x => x.Id == id);

                if (existingDifficulty == null)
                {
                    return null;
                }

                context.Difficulties.Remove(existingDifficulty);
                await context.SaveChangesAsync();
                return existingDifficulty;
            }
        }

        public async Task<List<Difficulty>> GetAllAsync()
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                return await context.Difficulties.ToListAsync();
            }
        }

        public async Task<Difficulty> GetByIdAsync(Guid id)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                return await context.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<Difficulty> UpdateAsync(Guid id, Difficulty difficulty)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                var existingDifficulty = await context.Difficulties.FirstOrDefaultAsync(x => x.Id == id);

                if (existingDifficulty == null)
                {
                    return null;
                }

                existingDifficulty.Name = difficulty.Name;

                await context.SaveChangesAsync();
                return existingDifficulty;
            }
        }
    }
}
