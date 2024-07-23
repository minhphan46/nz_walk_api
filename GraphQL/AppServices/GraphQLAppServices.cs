using NZWalks.GraphQL.DataLoaders;
using NZWalks.GraphQL.Resolvers;
using NZWalks.GraphQL.Schema.Mutations;
using NZWalks.GraphQL.Schema.Queries;
using NZWalks.GraphQL.Schema.Subscriptions;

namespace NZWalks.GraphQL.GraphQLAppServices
{
    public class GraphQLAppServices
    {
        public static WebApplicationBuilder AppBuilder(WebApplicationBuilder builder)
        {
            // Resolver
            builder.Services.AddScoped<CategoriesResolver>();
            builder.Services.AddScoped<DifficultiesResolver>();
            builder.Services.AddScoped<RegionsResolver>();
            builder.Services.AddScoped<WalksResolver>();
            builder.Services.AddScoped<WalkCategoriesResolver>();

            // DataLoader
            builder.Services.AddScoped<DifficultyDataLoader>();
            builder.Services.AddScoped<RegionDataLoader>();
            builder.Services.AddScoped<WalkDataLoader>();
            builder.Services.AddScoped<WalkCategorieDataLoader>();

            // QueryType
            builder.Services
                .AddGraphQLServer()
                .AddQueryType(q => q.Name("Query"))
                .AddType<CategoriesQuery>()
                .AddType<DifficultyQuery>()
                .AddType<RegionQuery>()
                .AddType<WalkQuery>()
                .AddMutationType(q => q.Name("Mutation"))
                .AddMutationConventions()
                .AddType<CategoriesMutation>()
                .AddSubscriptionType(s => s.Name("Subscription"))
                .AddType<CategoriesSubscription>()
                .AddInMemorySubscriptions()
                ;

            return builder;
        }
    }
}
