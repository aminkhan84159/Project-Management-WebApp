using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nexus.API.DataService.IDataService;
using Nexus.API.Manager.Validations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Nexus.API.Manager.Manager
{
    public class LoginManager 
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginManager(
            ILoginService loginService, 
            IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        public async Task<string> UserLogin(string info, string password)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            if (await _loginService.GetUserByInfo(info) == null)
                validations.Add("Email or Username Invalid", "User with this email/username doesn't exist");

            SHA256 hash = SHA256.Create();
            var encoding = Encoding.Default.GetBytes(password);
            var hashed = hash.ComputeHash(encoding);
            var hashedPassword = Convert.ToHexString(hashed);

            if (validations.Count == 0)
            {
                var user = await _loginService.UserLogin(info, hashedPassword);

                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("Email",user.Email.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(30),
                        signingCredentials: signIn
                        );
                    string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                    return tokenValue.ToString();
                }
                else
                {
                    validations.Add("User Invalid", "Email or Password is incorrect");
                    validations.Add("Login", "Login failed");

                    throw new CustomValidation(validations);
                }
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }
    }
}
