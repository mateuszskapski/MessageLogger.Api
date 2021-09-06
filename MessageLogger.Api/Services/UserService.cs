using MessageLogger.Api.Controllers;
using MessageLogger.Api.Models;
using MessageLogger.Api.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace MessageLogger.Api.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly List<User> _users = new()
        {
            new User
            {
                UserId = 1,
                FirstName = "Mateusz",
                LastName = "Skapski",
                Email = "mateusz.skapski@gmail.com",
                UserName = "mateusz.skapski@gmail.com",
                Password = "demo"
            }
        };

        public UserService(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator ?? throw new System.ArgumentNullException(nameof(tokenGenerator));
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            var user = _users.SingleOrDefault(u => u.UserName == request.UserName && u.Password == request.Password);

            if (user is null)
            {
                return null;
            }

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthenticationResponse(user, token);
        }

        public User GetUserById(int userId) => _users.FirstOrDefault(u => u.UserId == userId);
    }
}
