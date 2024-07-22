using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using NZWalks.GraphQL.DTOs.Categories;

namespace NZWalks.GraphQL.Schema.Subscriptions
{
    [ExtendObjectType("Subscription")]
    public class CategoriesSubscription
    {
        [Subscribe]
        public CategoryOutput CategoryCreated([EventMessage] CategoryOutput category) => category;

        public ValueTask<ISourceStream<CategoryOutput>> SubscribeToCategoryUpdated(Guid categoryId, [Service] ITopicEventReceiver receiver)
        {
            string topicName = $"{categoryId}_{nameof(CategoriesSubscription.CategoryUpdated)}";

            return receiver.SubscribeAsync<CategoryOutput>(topicName);
        }

        [Subscribe(With = nameof(SubscribeToCategoryUpdated))]
        public CategoryOutput CategoryUpdated([EventMessage] CategoryOutput category) => category;
    }
}
