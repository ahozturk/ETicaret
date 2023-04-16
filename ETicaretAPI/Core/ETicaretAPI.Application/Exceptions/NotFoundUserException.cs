using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : this("Kullanıcı Adı veya Şifre Hatalı!")
        {

        }

        public NotFoundUserException(string errorMessage)
        {

        }
    }
}
