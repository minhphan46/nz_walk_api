using AppAny.HotChocolate.FluentValidation;
using NZWalks.Entities;
using NZWalks.GraphQL.DTOs.Categories;
using NZWalks.GraphQL.Resolvers;
using NZWalks.GraphQL.Schema.Subscriptions;
using NZWalks.GraphQL.Validators;
using AutoMapper;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;

namespace NZWalks.GraphQL.Schema.Mutations
{
    [ExtendObjectType("Mutation")]
    public class CategoriesMutation
    {

        private readonly CategoriesResolver _resolver;
        private readonly IMapper mapper;

        public CategoriesMutation(CategoriesResolver resolver, IMapper mapper)
        {
            _resolver = resolver;
            this.mapper = mapper;
        }

        // [Authorize]
        public async Task<CategoryOutput> CreateCategory([UseFluentValidation, UseValidator<CategoryInputValidator>] CategoryInput categoryInput)
        {
            var categoryDomain = mapper.Map<Category>(categoryInput);

            categoryDomain = await _resolver.CreateAsync(categoryDomain);

            var categoryOutput = mapper.Map<CategoryOutput>(categoryDomain);

            return categoryOutput;
        }

        [Authorize]
        public async Task<CategoryOutput> UpdateCategory(Guid categoryId, [UseFluentValidation] CategoryInput categoryInput)
        {
            var categoryDomain = mapper.Map<Category>(categoryInput);
            // check if the category exists
            categoryDomain = await _resolver.UpdateAsync(categoryId, categoryDomain);

            if (categoryDomain == null)
            {
                throw new GraphQLException(new Error("Category not found.", "CATEGORY_NOT_FOUND"));
            }

            var categoryOutput = mapper.Map<CategoryOutput>(categoryDomain);

            return categoryOutput;
        }

        [Authorize(Policy = "IsAdmin")]
        public async Task<CategoryOutput> DeleteCategory(Guid categoryId)
        {
            var categoryDomainModel = await _resolver.DeleteAsync(categoryId);

            if (categoryDomainModel == null)
            {
                throw new GraphQLException(new Error("Category not found.", "CATEGORY_NOT_FOUND"));
            }

            var categoryOutput = mapper.Map<CategoryOutput>(categoryDomainModel);


            return categoryOutput;
        }

        [Authorize]
        public async Task<CategoryOutput> CreateCategorySubscription([UseFluentValidation] CategoryInput categoryInput, [Service] ITopicEventSender eventSender)
        {
            var categoryDomain = mapper.Map<Category>(categoryInput);

            categoryDomain = await _resolver.CreateAsync(categoryDomain);

            var categoryOutput = mapper.Map<CategoryOutput>(categoryDomain);

            await eventSender.SendAsync(nameof(CategoriesSubscription.CategoryCreated), categoryOutput);

            return categoryOutput;
        }

        [Authorize]
        public async Task<CategoryOutput> UpdateCategorySubscription(Guid categoryId, [UseFluentValidation] CategoryInput categoryInput, [Service] ITopicEventSender eventSender)
        {
            var categoryDomain = mapper.Map<Category>(categoryInput);
            // check if the category exists
            categoryDomain = await _resolver.UpdateAsync(categoryId, categoryDomain);

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
