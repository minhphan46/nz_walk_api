using Microsoft.EntityFrameworkCore;
using UdemyProject1.Data;
using UdemyProject1.Models.Domain;
using UdemyProject1.Repositories.Interfaces;

namespace UdemyProject1.Repositories.Implements
{
    public class SQLTagRepository : ITagRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLTagRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> DeleteAsync(Guid id)
        {
            var existingTag = await dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTag == null)
            {
                return null;
            }

            // remove all walk tags associated with this tag
            var walkTags = dbContext.WalkTags.Where(wt => wt.WalkId == id).ToList();
            dbContext.WalkTags.RemoveRange(walkTags);

            dbContext.Tags.Remove(existingTag);
            await dbContext.SaveChangesAsync();
            return existingTag;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await dbContext.Tags.ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            return await dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag> UpdateAsync(Guid id, Tag tag)
        {
            var existingTag = await dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTag == null)
            {
                return null;
            }

            existingTag.Name = tag.Name;

            await dbContext.SaveChangesAsync();
            return existingTag;
        }
    }
}
