using Microsoft.EntityFrameworkCore;
using UdemyProject1.Data.DbContexts;
using UdemyProject1.Entities;
using UdemyProject1.GraphQL.Resolvers;
using UdemyProject1.RESTful.Repositories.Interfaces;

namespace UdemyProject1.GraphQL.Schema.Queries
{
    [ExtendObjectType("Query")]
    public class CategoriesQuery
    {
        private readonly IDbContextFactory<NZWalksDbContext> _dbContextFactory;
        private readonly CategoriesResolver _resolver;

        public CategoriesQuery(IDbContextFactory<NZWalksDbContext> dbContextFactory, CategoriesResolver resolver)
        {
            _dbContextFactory = dbContextFactory;
            _resolver = resolver;
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                var categories = await _resolver.GetAllAsync(context);
                return categories;
            }
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                var category = await _resolver.GetByIdAsync(context, categoryId);

                if (category == null)
                {
                    return null;
                }
                return category;
            }

        }
    }
}
