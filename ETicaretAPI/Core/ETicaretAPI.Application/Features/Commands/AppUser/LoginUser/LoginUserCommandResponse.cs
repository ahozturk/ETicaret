using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
    }

    public class LoginUserSuccessResponse : LoginUserCommandResponse
    {
        public DTOs.Token Token { get; set; }

    }
    public class LoginUserErrorResponse : LoginUserCommandResponse
    {
        public string ErrorMessage { get; set; }
        public LoginUserErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
