using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task21_9
{
    public class AuthOptions
    {
        public const string ISSUER = "Task21_9Server"; //publisher of token
        public const string AUDIENCE = "Task21_9Client"; //client of token
        const string KEY = "mysupersecret_secretkey!123"; //security key
        public const int LIFETIME = 1; //time of life - 1 minute
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
