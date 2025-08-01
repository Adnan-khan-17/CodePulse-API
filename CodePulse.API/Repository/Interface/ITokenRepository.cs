using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.Repository.Interface
{
    public interface ITokenRepository

    {
        public string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
