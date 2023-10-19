using SoftwareControle.Models;

namespace SoftwareControle.Site.APICall.Authenticacao
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel?> Login(AuthenticationUserModel userForAuthentication);
        Task Logout();
    }
}