using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UdemyProject1.Data.DbContexts;
using UdemyProject1.Entities;
using UdemyProject1.GraphQL.Resolvers;
using UdemyProject1.RESTful.Repositories.Interfaces;

namespace UdemyProject1.GraphQL.Schema.Mutations
{
    [ExtendObjectType("Mutation")]
    public class CategoriesMutation
    {
        private readonly IDbContextFactory<NZWalksDbContext> _dbContextFactory;
        private readonly CategoriesResolver _resolver;
        private readonly IMapper mapper;

        public CategoriesMutation(IDbContextFactory<NZWalksDbContext> dbContextFactory, CategoriesResolver resolver, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _resolver = resolver;
            this.mapper = mapper;
        }

        public async Task<CategoryOutput> CreateCategory(CategoryInput categoryInput)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                var categoryDomain = mapper.Map<Category>(categoryInput);

                categoryDomain = await _resolver.CreateAsync(context, categoryDomain);

                var categoryOutput = mapper.Map<CategoryOutput>(categoryDomain);

                return categoryOutput;
            }
        }

        public async Task<CategoryOutput> UpdateCategory(Guid categoryId, CategoryInput categoryInput)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                var categoryDomain = mapper.Map<Category>(categoryInput);
                // check if the category exists
                categoryDomain = await _resolver.UpdateAsync(context, categoryId, categoryDomain);

                if (categoryDomain == null)
                {
                    return null;
                }

                var categoryOutput = mapper.Map<CategoryOutput>(categoryDomain);

                return categoryOutput;
            }
        }

        public async Task<CategoryOutput> DeleteCategory(Guid categoryId)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                var categoryDomainModel = await _resolver.DeleteAsync(context, categoryId);

                if (categoryDomainModel == null)
                {
                    return null;
                }

                var categoryOutput = mapper.Map<CategoryOutput>(categoryDomainModel);


                return categoryOutput;
            }
        }
    }
}
