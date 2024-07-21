using Microsoft.AspNetCore.Identity;

namespace NZWalks.RESTful.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
