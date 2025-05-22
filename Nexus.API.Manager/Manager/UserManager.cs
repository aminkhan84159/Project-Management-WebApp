using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Validations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Nexus.API.Manager.Manager
{
    public class UserManager 
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _configuration;

        public UserManager(
            IUserService userService, 
            IHttpContextAccessor accessor, 
            IConfiguration configuration)
        {
            _userService = userService;
            _accessor = accessor;
            _configuration = configuration;
        }

        public async Task<List<UserOutputDto>> GetAllUsers()
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var user = await _userService.GetAllUsers();

            if (user != null)
            {
                var res = user.Select(x => UserOutputDto.MapToEntity(x)).ToList();

                return res;
            }
            else
            {
                validations.Add("List is empty", "There are no users");

                throw new CustomValidation(validations);
            }
        }

        public async Task<string> AddUser(UserDto userDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            Regex emailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                                         RegexOptions.CultureInvariant | RegexOptions.Singleline);
            Match email = emailRegex.Match(userDto.Email);

            if (email.Success == false)
                validations.Add("Invalid Email", "Email syntax : yourname@domain.com");

            if (string.IsNullOrWhiteSpace(userDto.Email))
                validations.Add("Email Null", "Email is missing");

            if (userDto.Email.Length > 80)
                validations.Add("Email lenght", "Maximum 80 characters are allowed");

            if (await _userService.GetUserByEmail(userDto.Email) != null)
                validations.Add("Email exist", "Email already exist , Choose another Email");

            Regex usernameRegex = new Regex(@"^[a-zA-Z0-9_]+$");
            Match username = usernameRegex.Match(userDto.Username);

            if (username.Success == false)
                validations.Add("Invalid Username", "Username must be a combination of (letters,digits,_) and should not contain white spaces");

            if (string.IsNullOrWhiteSpace(userDto.Username))
                validations.Add("Username null", "Username is missing");

            if (userDto.Username.Length > 50)
                validations.Add("Username lenght", "Maximum 50 characters are allowed");

            if (await _userService.GetUserByUsername(userDto.Username) != null)
                validations.Add("Username exist", "Username already exist, Choose another username");

            Regex passwordRegex = new Regex(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,}$");
            Match passowrd = passwordRegex.Match(userDto.Password);

            if (passowrd.Success == false)
                validations.Add("Password Invalid", "Passowrd must be combination of digits , one uppercase letter , one lowercase letter, and one special character");

            if (string.IsNullOrWhiteSpace(userDto.Password))
                validations.Add("Password null", "Password is missing");

            if (userDto.Password.Length < 8 || userDto.Password.Length > 32)
                validations.Add("Password length", "Minimum 8 and Maximum 32 characters are allowed");

            SHA256 hash = SHA256.Create();
            var encoding = Encoding.Default.GetBytes(userDto.Password);
            var hashed = hash.ComputeHash(encoding);
            var hashedPassword = Convert.ToHexString(hashed);


            if (validations.Count == 0)
            {
                var user = new User
                {
                    Email = userDto.Email,
                    Username = userDto.Username,
                    Password = hashedPassword,
                    JoinedDate = DateOnly.FromDateTime(DateTime.Now),
                    CreatedOn = DateTime.UtcNow,
                    //CreatedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value)
                };

                var currentUser = await _userService.AddUser(user);

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
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signIn
                    );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenValue.ToString();
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<UserOutputDto> UpdateUser(int userId, UserDto userDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var user = await _userService.GetUserById(userId);

            if (user != null)
            {


                if (userDto.Email != user.Email)
                {
                    Regex emailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                                                 RegexOptions.CultureInvariant | RegexOptions.Singleline);
                    Match email = emailRegex.Match(userDto.Email);

                    if (email.Success == false)
                        validations.Add("Invalid Email", "Email syntax : yourname@domain.com");

                    if (string.IsNullOrWhiteSpace(userDto.Email))
                        validations.Add("Email Null", "Email is missing");

                    if (userDto.Email.Length > 80)
                        validations.Add("Email lenght", "Maximum 80 characters are allowed");

                    if (await _userService.GetUserByEmail(userDto.Email) != null)
                        validations.Add("Email exist", "Email already exist , Choose another Email");
                }

                if (userDto.Username != user.Username)
                {

                    Regex usernameRegex = new Regex(@"^[a-zA-z0-9_]+$");
                    Match username = usernameRegex.Match(userDto.Username);

                    if (username.Success == false)
                        validations.Add("Invalid Username", "Username must be a combination of (letters,digits,_) and should not contain white spaces");

                    if (string.IsNullOrWhiteSpace(userDto.Username))
                        validations.Add("Username null", "Username is missing");

                    if (userDto.Username.Length > 50)
                        validations.Add("Username lenght", "Maximum 50 characters are allowed");

                    if (await _userService.GetUserByUsername(userDto.Username) != null)
                        validations.Add("Username exist", "Username already exist, Choose another username");
                }

                Regex passwordRegex = new Regex(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,}$");
                Match passowrd = passwordRegex.Match(userDto.Password);

                if (passowrd.Success == false)
                    validations.Add("Password Invalid", "Passowrd must be combination of digits , one uppercase and one lowercase letter, and one special character");

                if (string.IsNullOrWhiteSpace(userDto.Password))
                    validations.Add("Password null", "Password is missing");

                if (userDto.Password.Length < 8 || userDto.Password.Length > 32)
                    validations.Add("Password length", "Minimum 8 and Maximum 32 characters are allowed");
            }
            else
            {
                validations.Add("Invalid Id", "User with this Id doesn't exist");
            }

            SHA256 hash = SHA256.Create();
            var encoding = Encoding.Default.GetBytes(userDto.Password);
            var hashed = hash.ComputeHash(encoding);
            var hashedPassword = Convert.ToHexString(hashed);

            if (validations.Count == 0)
            {
                user.Email = userDto.Email;
                user.Username = userDto.Username;
                user.Password = hashedPassword;
                user.ChangedOn = DateTime.UtcNow;
                user.ChangedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                await _userService.UpdateUser(user);

                return UserOutputDto.MapToEntity(user);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<UserOutputDto> DeleteUser(int userId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var user = await _userService.GetUserById(userId);

            if (user != null)
            {
                await _userService.DeleteUser(user);

                return UserOutputDto.MapToEntity(user);
            }
            else
            {
                validations.Add("Invalid Id", "User with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<UserOutputDto> GetUserById(int userId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var user = await _userService.GetUserById(userId);

            if (user != null)
            {
                return UserOutputDto.MapToEntity(user);
            }
            else
            {
                validations.Add("Invalid Id", "User with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }
    }
}
