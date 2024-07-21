using Microsoft.EntityFrameworkCore;
using UdemyProject1.Data.DbContexts;
using UdemyProject1.Entities;

namespace UdemyProject1.GraphQL.Resolvers
{
    public class CategoriesResolver
    {
        public async Task<Category> CreateAsync(NZWalksDbContext dbContext, Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteAsync(NZWalksDbContext dbContext, Guid id)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            // remove all walk categorys associated with this category
            var walkCategories = dbContext.WalkCategories.Where(wt => wt.WalkId == id).ToList();
            dbContext.WalkCategories.RemoveRange(walkCategories);

            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<List<Category>> GetAllAsync(NZWalksDbContext dbContext)
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(NZWalksDbContext dbContext, Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> UpdateAsync(NZWalksDbContext dbContext, Guid id, Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;

            await dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
