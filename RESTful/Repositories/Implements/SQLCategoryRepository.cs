using Microsoft.EntityFrameworkCore;
using NZWalks.Data.DbContexts;
using NZWalks.Entities;
using NZWalks.RESTful.Repositories.Interfaces;

namespace NZWalks.RESTful.Repositories.Implements
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLCategoryRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteAsync(Guid id)
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

        public async Task<List<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> UpdateAsync(Guid id, Category category)
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
