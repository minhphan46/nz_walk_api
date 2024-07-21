using AutoMapper;
using UdemyProject1.Entities;
using UdemyProject1.RESTful.Repositories.Interfaces;

namespace UdemyProject1.GraphQL.Schema.Mutations
{
    [ExtendObjectType("Mutation")]
    public class CategoriesMutation
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoriesMutation(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<CategoryOutput> CreateCategory(CategoryInput categoryInput)
        {
            var categoryDomain = mapper.Map<Category>(categoryInput);

            categoryDomain = await categoryRepository.CreateAsync(categoryDomain);

            var categoryOutput = mapper.Map<CategoryOutput>(categoryDomain);

            return categoryOutput;
        }

        public async Task<CategoryOutput> UpdateCategory(Guid categoryId, CategoryInput categoryInput)
        {
            var categoryDomain = mapper.Map<Category>(categoryInput);
            // check if the category exists
            categoryDomain = await categoryRepository.UpdateAsync(categoryId, categoryDomain);

            if (categoryDomain == null)
            {
                return null;
            }

            var categoryOutput = mapper.Map<CategoryOutput>(categoryDomain);

            return categoryOutput;
        }

        public async Task<CategoryOutput> DeleteCategory(Guid categoryId)
        {
            var categoryDomainModel = await categoryRepository.DeleteAsync(categoryId);

            if (categoryDomainModel == null)
            {
                return null;
            }

            var categoryOutput = mapper.Map<CategoryOutput>(categoryDomainModel);


            return categoryOutput;
        }
    }
}
