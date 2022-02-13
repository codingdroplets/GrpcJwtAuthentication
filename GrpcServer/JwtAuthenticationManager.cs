using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GrpcServer
{
    public static class JwtAuthenticationManager
    {
        public const string JWT_TOKEN_KEY = "CodingDroplets@2022";
        private const int JWT_TOKEN_VALIDITY = 30;

        public static AuthenticationResponse Authenticate(AuthenticationRequest authenticationRequest)
        {
            // -- Implement User Credentials Validation
            var userRole = string.Empty;
            if(authenticationRequest.UserName == "admin" && authenticationRequest.Password == "admin")
            {
                userRole = "Administrator";
            }
            else if(authenticationRequest.UserName == "user" && authenticationRequest.Password == "user")
            {
                userRole = "User";
            }
            else
            {
                return null;
            }
                

            //------------------------------------


            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(JWT_TOKEN_KEY);
            var tokenExpiryDateTime = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", authenticationRequest.UserName),
                    new Claim(ClaimTypes.Role, userRole)
                }),
                Expires = tokenExpiryDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                AccessToken = token,
                ExpiresIn = (int)tokenExpiryDateTime.Subtract(DateTime.Now).TotalSeconds
            };
        }
    }
}
