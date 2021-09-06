using MessageLogger.Api.Models;

namespace MessageLogger.Api.Utilities
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}