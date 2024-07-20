using UdemyProject1.Entities;
using UdemyProject1.RESTful.Repositories.Interfaces;

namespace UdemyProject1.GraphQL.Schema.Queries
{
    [ExtendObjectType("Query")]
    public class CategoriesQuery
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesQuery(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            var categories = await categoryRepository.GetAllAsync();
            return categories;
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            var category = await categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                return null;
            }
            return category;
        }
    }
}
