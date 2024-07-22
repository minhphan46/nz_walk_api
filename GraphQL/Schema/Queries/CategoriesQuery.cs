using Microsoft.EntityFrameworkCore;
using NZWalks.Data.DbContexts;
using NZWalks.Entities;
using NZWalks.GraphQL.Resolvers;
using NZWalks.RESTful.Repositories.Interfaces;

namespace NZWalks.GraphQL.Schema.Queries
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
                    throw new GraphQLException(new Error("Category not found.", "CATEGORY_NOT_FOUND"));
                }
                return category;
            }

        }
    }
}
