using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandle : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandle(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser appUser = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (appUser == null)
                throw new NotFoundUserException();
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);

            if (result.Succeeded)
            {
                return new LoginUserSuccessResponse()
                {
                    Token = _tokenHandler.CreateAccessToken(15)
                };

            }
            //return new LoginUserErrorResponse("Error When Token Creating!");
            throw new AuthenticationErrorException();
        }
    }
}
