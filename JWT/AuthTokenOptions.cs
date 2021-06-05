using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace server.JWT
{
    public class AuthTokenOptions
    {
        public const string ISSUER = "SmartHome"; // issuer

        public const string AUDIENCE = "http://localhost:58335/"; // audience

        const string KEY = "thisisasecretkeyissuer";
        
        public const int LIFETIME = 17280; // TTL 12 days, consider about less time
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
