using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string p)
        {
            return p
                .Replace("ç", "c")
                .Replace("Ç", "C")
                .Replace("ö", "o")
                .Replace("Ö", "O")
                .Replace("ı", "i")
                .Replace("İ", "I")
                .Replace("ş", "s")
                .Replace("Ş", "S")
                .Replace("ü", "u")
                .Replace("Ü", "U")
                .Replace("ğ", "g")
                .Replace("Ğ", "G")
                .Replace("\"", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("/", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Replace("*", "")
                .Replace(",", "")
                .Replace(".", "")
                .Replace(":", "")
                .Replace(";", "")
                .Replace("|", "");
        }
    }
}
