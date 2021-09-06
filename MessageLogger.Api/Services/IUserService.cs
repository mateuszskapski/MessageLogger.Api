using MessageLogger.Api.Models;

namespace MessageLogger.Api.Services
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
        User GetUserById(int userId);
    }
}