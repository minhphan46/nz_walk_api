using UdemyProject1.GraphQL.Schema.Queries;

namespace UdemyProject1.GraphQL.GraphQLAppServices
{
    public class GraphQLAppServices
    {
        public static WebApplicationBuilder AppBuilder(WebApplicationBuilder builder)
        {
            builder.Services
                .AddGraphQLServer()
                .AddQueryType(q => q.Name("Query"))
                .AddType<Query>()
                .AddType<CategoriesQuery>();

            return builder;
        }
    }
}
