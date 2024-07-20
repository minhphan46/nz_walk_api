namespace UdemyProject1.GraphQL.GraphQLAppServices
{
    public class GraphQLAppServices
    {
        public static WebApplicationBuilder AppBuilder(WebApplicationBuilder builder)
        {
            builder.Services
                .AddGraphQLServer();

            return builder;
        }
    }
}
