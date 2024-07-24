using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using Google.Apis.Auth.OAuth2;
using NZWalks.GraphQL.DataLoaders;
using NZWalks.GraphQL.DTOs.Categories;
using NZWalks.GraphQL.DTOs.Difficulties;
using NZWalks.GraphQL.DTOs.Regions;
using NZWalks.GraphQL.DTOs.Walks;
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
                .AddType<WalkOutput>()
                .AddType<DifficultyOutput>()
                .AddType<RegionOutput>()
                .AddType<CategoryOutput>()
                .AddQueryType(q => q.Name("Query"))
                .AddType<CategoriesQuery>()
                .AddType<DifficultyQuery>()
                .AddType<RegionQuery>()
                .AddType<WalkQuery>()
                .AddType<SearchQuery>()
                .AddMutationType(q => q.Name("Mutation"))
                .AddMutationConventions()
                .AddType<CategoriesMutation>()
                .AddSubscriptionType(s => s.Name("Subscription"))
                .AddType<CategoriesSubscription>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions()
                .AddAuthorization()
                ;

            // Authentication
            var firebaseConfigPath = Environment.GetEnvironmentVariable("FIREBASE_CONFIG");

            builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(firebaseConfigPath)
            }));
            builder.Services.AddFirebaseAuthentication();
            builder.Services.AddAuthorization(
                o => o.AddPolicy("IsAdmin", p => p.RequireClaim(FirebaseUserClaimType.EMAIL, "test@test.com"))
            );

            return builder;
        }
    }
}
