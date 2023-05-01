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
        readonly Abstractions.Services.Authentications.IInternalAuthentication _internalAuthentication;

        public LoginUserCommandHandle(Abstractions.Services.Authentications.IInternalAuthentication internalAuthentication)
        {
            _internalAuthentication = internalAuthentication;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            return new()
            {
                Token = await _internalAuthentication.LoginAsync(request.UsernameOrEmail, request.Password, 1000)
            };
        }
    }
}
