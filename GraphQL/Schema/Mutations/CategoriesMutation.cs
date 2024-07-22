using AutoMapper;
using HotChocolate.Execution.Processing;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data.DbContexts;
using NZWalks.Entities;
using NZWalks.GraphQL.Resolvers;
using NZWalks.GraphQL.Schema.Subscriptions;
using NZWalks.RESTful.Repositories.Interfaces;

namespace NZWalks.GraphQL.Schema.Mutations
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
                    throw new GraphQLException(new Error("Category not found.", "CATEGORY_NOT_FOUND"))
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
                    throw new GraphQLException(new Error("Category not found.", "CATEGORY_NOT_FOUND"));
                }

                var categoryOutput = mapper.Map<CategoryOutput>(categoryDomainModel);


                return categoryOutput;
            }
        }

        public async Task<CategoryOutput> CreateCategorySubscription(CategoryInput categoryInput, [Service] ITopicEventSender eventSender)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                var categoryDomain = mapper.Map<Category>(categoryInput);

                categoryDomain = await _resolver.CreateAsync(context, categoryDomain);

                var categoryOutput = mapper.Map<CategoryOutput>(categoryDomain);

                await eventSender.SendAsync(nameof(CategoriesSubscription.CategoryCreated), categoryOutput);

                return categoryOutput;
            }
        }

        public async Task<CategoryOutput> UpdateCategorySubscription(Guid categoryId, CategoryInput categoryInput, [Service] ITopicEventSender eventSender)
        {
            using (NZWalksDbContext context = _dbContextFactory.CreateDbContext())
            {
                var categoryDomain = mapper.Map<Category>(categoryInput);
                // check if the category exists
                categoryDomain = await _resolver.UpdateAsync(context, categoryId, categoryDomain);

                if (categoryDomain == null)
                {
                    throw new GraphQLException(new Error("Category not found.", "CATEGORY_NOT_FOUND"));
                }

                var categoryOutput = mapper.Map<CategoryOutput>(categoryDomain);

                string updateCategoryTopic = $"{categoryOutput.Id}_{nameof(CategoriesSubscription.CategoryUpdated)}";
                await eventSender.SendAsync(updateCategoryTopic, categoryOutput);

                return categoryOutput;
            }
        }
    }
}
