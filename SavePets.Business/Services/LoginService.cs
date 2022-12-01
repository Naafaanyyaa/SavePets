using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using SavePets.Business.Infrastructure;
using SavePets.Business.Interfaces;
using SavePets.Business.Models.Requests;
using SavePets.Business.Models.Response;
using SavePets.Data.Entities.Identity;

namespace SavePets.Business.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;

        private readonly JwtHandler _jwtHandler;

        public LoginService(UserManager<User> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<AuthorizeResponse> SignInAsync(AuthenticateRequest request)
        {

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new AuthorizeResponse()
                {
                    ErrorMessage = "Invalid Authentication"
                };
            }

            if (user.IsBanned == true)
            {
                return new AuthorizeResponse()
                {
                    ErrorMessage = "User is banned"
                };
            }

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = await _jwtHandler.GetClaimsAsync(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new AuthorizeResponse() { IsAuthSuccessful = true, Token = token};
        }
    }

   
}
