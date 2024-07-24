using AutoMapper;
using HotChocolate.Authorization;
using NZWalks.Entities;
using NZWalks.GraphQL.DTOs.Walks;
using NZWalks.Middlewares.UseUser;

namespace NZWalks.GraphQL.Schema.Queries
{
    [ExtendObjectType("Query")]
    public class UserQuery
    {
        [Authorize]
        [UseUser]
        public async Task<User> GetMe([User] User user)
        {
            return user;
        }
    }
}
