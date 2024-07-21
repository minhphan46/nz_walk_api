using UdemyProject1.GraphQL.Resolvers;
using UdemyProject1.GraphQL.Schema.Mutations;
using UdemyProject1.GraphQL.Schema.Queries;

namespace UdemyProject1.GraphQL.GraphQLAppServices
{
    public class GraphQLAppServices
    {
        public static WebApplicationBuilder AppBuilder(WebApplicationBuilder builder)
        {
            // Resolver
            builder.Services.AddScoped<CategoriesResolver>();

            // QueryType
            builder.Services
                .AddGraphQLServer()
                .AddQueryType(q => q.Name("Query"))
                .AddType<CategoriesQuery>()
                .AddMutationType(q => q.Name("Mutation"))
                .AddMutationConventions()
                .AddType<CategoriesMutation>();

            return builder;
        }
    }
}
