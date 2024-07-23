using NZWalks.Entities;
using NZWalks.GraphQL.Resolvers;

namespace NZWalks.GraphQL.Schema.Queries
{
    [ExtendObjectType("Query")]
    public class CategoriesQuery
    {
        private readonly CategoriesResolver _resolver;

        public CategoriesQuery(CategoriesResolver resolver)
        {
            _resolver = resolver;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _resolver.GetAllAsync();
            return categories;
        }

        public async Task<Category> GetCategory(Guid id)
        {
            var category = await _resolver.GetByIdAsync(id);

            if (category == null)
            {
                throw new GraphQLException(new Error("Category not found.", "CATEGORY_NOT_FOUND"));
            }
            return category;
        }
    }
}
