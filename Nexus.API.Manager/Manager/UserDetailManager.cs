using Microsoft.AspNetCore.Http;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Validations;
using System.Text.RegularExpressions;

namespace Nexus.API.Manager.Manager
{
    public class UserDetailManager
    {
        private readonly IUserDetailService _userDetailService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UserDetailManager(
            IUserDetailService userDetailService, 
            IHttpContextAccessor accessor,
            IUserService userService,
            IEmailService emailService)
        {
            _userDetailService = userDetailService;
            _accessor = accessor;
            _userService = userService;
            _emailService = emailService;
        }

        public async Task<List<UserDetailOutputDto>> GetAllUserDetails()
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var userDetail = await _userDetailService.GetAllUserDetails();

            if (userDetail != null)
            {
                var res = userDetail.Select(x => UserDetailOutputDto.MapToEntity(x)).ToList();

                return res;
            }
            else
            {
                validations.Add("UsrDetail", "No UserDetails");

                throw new CustomValidation(validations);
            }
        }

        public async Task<UserDetailOutputDto> AddUserDetail(UserDetailDto userDetailDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            /*if (await _service.GetUserById(userDetailDto.UserId) == null)
                validations.Add("UserId Invalid", "User with this Id doesn't exist");*/

            Regex nameRegex = new Regex(@"^[a-zA-z\S]+$");
            Match firstname = nameRegex.Match(userDetailDto.FirstName);

            if (firstname.Success == false)
                validations.Add("Firstname Invalid", "Firstname must not contain white spaces");

            if (string.IsNullOrWhiteSpace(userDetailDto.FirstName))
                validations.Add("Firstname null", "Firstname is mising");

            if (userDetailDto.FirstName.Length > 25)
                validations.Add("Firstname length", "Maximun 25 characters allowed");

            Match lastname = nameRegex.Match(userDetailDto.LastName);

            if (lastname.Success == false)
                validations.Add("Lastname Invalid", "Lastname must not contain white spaces");

            if (string.IsNullOrWhiteSpace(userDetailDto.LastName))
                validations.Add("Lastname null", "Lastname is mising");

            if (userDetailDto.LastName.Length > 25)
                validations.Add("Lastname length", "Maximun 25 characters allowed");

            Regex phoneRegex = new Regex(@"^[0-9\S]+$");
            Match phone = phoneRegex.Match(userDetailDto.PhoneNo);

            if (phone.Success == false)
                validations.Add("PhoneNo Invalid","Phone no must must only contain digits");

            if (await _userDetailService.GetUserDetailByPhoneNo(userDetailDto.PhoneNo) != null)
                validations.Add("PhoneNo exist", "PhoneNo already exist choose another phoneno");

            if (string.IsNullOrWhiteSpace(userDetailDto.PhoneNo))
                validations.Add("Phone null", "PhoneNo is missing");

            if (userDetailDto.PhoneNo.Length != 10)
                validations.Add("PhoneNo length","PhoneNo must contain 10 digits");

            if (string.IsNullOrWhiteSpace(userDetailDto.Address))
                validations.Add("Address null", "Address is missing");

            if (userDetailDto.Address.Length < 10 || userDetailDto.Address.Length > 255)
                validations.Add("Address length", "Minimum 10 characters and Maximum 255 characters are allowed");

            Regex stateRegex = new Regex(@"^[a-zA-Z]+$");
            Match state = stateRegex.Match(userDetailDto.State);

            if (state.Success == false)
                validations.Add("State Invalid", "State must not contain digits");

            if (string.IsNullOrWhiteSpace(userDetailDto.State))
                validations.Add("State null", "State is missing");

            if (userDetailDto.State.Length > 30)
                validations.Add("State length", "Maximum 30 Characters are allowed");

            Regex cityRegex = new Regex(@"^[[a-zA-z\s]+$");
            Match city = cityRegex.Match(userDetailDto.City);

            if (city.Success == false)
                validations.Add("City Invalid", "City must not contain digits");

            if (string.IsNullOrWhiteSpace(userDetailDto.City))
                validations.Add("City null", "City is missing");

            if (userDetailDto.City.Length > 30)
                validations.Add("City length", "Maximum 30 Characters are allowed");

            if (validations.Count == 0)
            {
                var userDetail = new UserDetail
                {
                    UserId = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value),
                    FirstName = userDetailDto.FirstName,
                    LastName = userDetailDto.LastName,
                    PhoneNo = userDetailDto.PhoneNo,
                    Gender = userDetailDto.Gender,
                    Age = userDetailDto.Age,
                    Address = userDetailDto.Address,
                    City = userDetailDto.City,
                    State = userDetailDto.State,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value)
                };

                var currentUserDetail = await _userDetailService.AddUserDetail(userDetail);

                var currentUser = await _userService.GetUserById(currentUserDetail.UserId);

                var emailSender = EmailDto.MapToEmail(currentUser, currentUserDetail);
                await _emailService.SendEmail(emailSender.recipient, emailSender.subject, emailSender.body);

                return UserDetailOutputDto.MapToEntity(userDetail);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<UserDetailOutputDto> UpdateUserDetail(int userDetailId, UserDetailDto userDetailDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var userDetail = await _userDetailService.GetUserDetailById(userDetailId);

            if (userDetail != null)
            {
                /*if (await _service.GetUserById(userDetailDto.UserId) == null)
                    validations.Add("UserId Invalid", "User with this Id doesn't exist");*/

                Regex nameRegex = new Regex(@"^[a-zA-z\S]+$");
                Match firstname = nameRegex.Match(userDetailDto.FirstName);

                if (firstname.Success == false)
                    validations.Add("Firstname Invalid", "Firstname must not contain white spaces");

                if (string.IsNullOrWhiteSpace(userDetailDto.FirstName))
                    validations.Add("Firstname null", "Firstname is mising");

                if (userDetailDto.FirstName.Length > 25)
                    validations.Add("Firstname length", "Maximun 25 characters allowed");

                Match lastname = nameRegex.Match(userDetailDto.LastName);

                if (lastname.Success == false)
                    validations.Add("Lastname Invalid", "Lastname must not contain white spaces");

                if (string.IsNullOrWhiteSpace(userDetailDto.LastName))
                    validations.Add("Lastname null", "Lastname is mising");

                if (userDetailDto.LastName.Length > 25)
                    validations.Add("Lastname length", "Maximun 25 characters allowed");

                if (userDetailDto.PhoneNo != userDetail.PhoneNo)
                {

                    Regex phoneRegex = new Regex(@"^[0-9\S]+$");
                    Match phone = phoneRegex.Match(userDetailDto.PhoneNo);

                    if (phone.Success == false)
                        validations.Add("PhoneNo Invalid", "Phone no must must only contain digits");

                    if (string.IsNullOrWhiteSpace(userDetailDto.PhoneNo))
                        validations.Add("Phone null", "PhoneNo is missing");

                    if (userDetailDto.PhoneNo.Length != 10)
                        validations.Add("PhoneNo length", "PhoneNo must contain 10 digits");

                    if (string.IsNullOrWhiteSpace(userDetailDto.Address))
                        validations.Add("Address null", "Address is missing");

                    if (userDetailDto.Address.Length < 10 || userDetailDto.Address.Length > 255)
                        validations.Add("Address length", "Minimum 10 characters and Maximum 255 characters are allowed");

                    if (await _userDetailService.GetUserDetailByPhoneNo(userDetailDto.PhoneNo) != null)
                        validations.Add("PhoneNo exist", "PhoneNo already exist choose another phoneno");
                }

                Regex stateRegex = new Regex(@"^[a-zA-Z]+$");
                Match state = stateRegex.Match(userDetailDto.State);

                if (state.Success == false)
                    validations.Add("State Invalid", "State must not contain digits");

                if (string.IsNullOrWhiteSpace(userDetailDto.State))
                    validations.Add("State null", "State is missing");

                if (userDetailDto.State.Length > 30)
                    validations.Add("State length", "Maximum 30 Characters are allowed");

                Regex cityRegex = new Regex(@"^[[a-zA-z\s]+$");
                Match city = cityRegex.Match(userDetailDto.City);

                if (city.Success == false)
                    validations.Add("City Invalid", "City must not contain digits");

                if (string.IsNullOrWhiteSpace(userDetailDto.City))
                    validations.Add("City null", "City is missing");

                if (userDetailDto.City.Length > 30)
                    validations.Add("City length", "Maximum 30 Characters are allowed");
            }
            else
            {
                validations.Add("UserDetail Id", "UserDetails wiht this Id doesn't exist");
            }

            if (validations.Count == 0)
            {
                userDetail.FirstName = userDetailDto.FirstName;
                userDetail.LastName = userDetailDto.LastName;
                userDetail.Gender = userDetailDto.Gender;
                userDetail.Age = userDetailDto.Age;
                userDetail.PhoneNo = userDetailDto.PhoneNo;
                userDetail.Address = userDetailDto.Address;
                userDetail.City = userDetailDto.City;
                userDetail.State = userDetailDto.State;
                userDetail.ChangedOn = DateTime.UtcNow;
                userDetail.ChangedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                await _userDetailService.UpdateUserDetail(userDetail);

                return UserDetailOutputDto.MapToEntity(userDetail);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<UserDetailOutputDto> DeleteUserDetail(int userDetailId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var userDetail = await _userDetailService.GetUserDetailById(userDetailId);

            if (userDetail != null)
            {
                await _userDetailService.DeleteUserDetail(userDetail);

                return UserDetailOutputDto.MapToEntity(userDetail);
            }
            else
            {
                validations.Add("UserDetail", "UserDetails with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<UserDetailOutputDto> GetUserDetailById(int userDetailId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var userDetail = await _userDetailService.GetUserDetailById(userDetailId);

            if (userDetail != null)
            {
                return UserDetailOutputDto.MapToEntity(userDetail);
            }
            else
            {
                validations.Add("UserDetail", "UserDetails with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<UserDetailOutputDto> GetUserDetailByUserId(int userId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var userDetail = await _userDetailService.GetUserDetailByUserId(userId);

            if (userDetail != null)
            {
                return UserDetailOutputDto.MapToEntity(userDetail);
            }
            else
            {
                validations.Add("User Id", "UserDetails with this user Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }
    }
}
