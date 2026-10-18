using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repository.TokenRepo
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, IList<string> roles);
    }
}
