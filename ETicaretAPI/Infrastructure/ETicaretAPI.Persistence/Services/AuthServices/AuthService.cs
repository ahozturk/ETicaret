using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Features.Commands.AppUser.LoginUser;
using ETicaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;

        readonly ITokenHandler _tokenHandler;
        readonly IConfiguration _configuration;
        readonly IUserService _userService;

        public AuthService(UserManager<AppUser> userManager,
            ITokenHandler tokenHandler,
            IConfiguration configuration,
            SignInManager<AppUser> signInManager,
            IUserService userService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }

        async Task<Token> CreateUserExternalAsync(AppUser user, string name, string email, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        NameSurname = name,
                        UserName = email
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, info);
            else
                throw new Exception("Invalid External Authentication");

            Token token = _tokenHandler.CreateAccessToken(15*60, user);
            await _userService.UpdateResfreshToken(token.RefreshToken, user, token.Expiration, 5);
            return token;
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int second)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _configuration["ExternalLoginSettings:Google:AppAudienceId"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, payload.Name, payload.Email, info, second);
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser appUser = await _userManager.FindByNameAsync(usernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (appUser == null)
                throw new NotFoundUserException();
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, appUser);
                await _userService.UpdateResfreshToken(token.RefreshToken, appUser, token.Expiration, 40);
                return token;

            }
            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshLoginAsync(string refreshToken)
        {
            AppUser? appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (appUser != null && appUser.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, appUser);
                await _userService.UpdateResfreshToken(token.RefreshToken, appUser, token.Expiration, 10);
                return token;
            }
            throw new AuthenticationErrorException();
        }
    }
}
