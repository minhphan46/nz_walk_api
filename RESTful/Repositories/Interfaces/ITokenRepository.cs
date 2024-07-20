using Microsoft.AspNetCore.Identity;

namespace UdemyProject1.RESTful.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
