using Microsoft.AspNetCore.Http;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;
using Nexus.API.Manager.Dto;
using Nexus.API.Manager.Validations;
using System.Text.RegularExpressions;

namespace Nexus.API.Manager.Manager
{
    public class StatusManager
    {
        private readonly IStatusService _statusService;
        private readonly IHttpContextAccessor _accessor;

        public StatusManager(
            IStatusService statusService, 
            IHttpContextAccessor accessor)
        {
            _statusService = statusService;
            _accessor = accessor;
        }

        public async Task<List<StatusOutputDto>> GetAllStatus()
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var status = await _statusService.GetAllStatus();

            if (status != null)
            {
                var res = status.Select(x => StatusOutputDto.MapToEntity(x)).ToList();

                return res;
            }
            else
            {
                validations.Add("Status", "No statuses");

                throw new CustomValidation(validations);
            }
        }

        public async Task<StatusOutputDto> AddStatus(StatusDto statusDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            Regex typeRegex = new Regex(@"^[a-zA-z\S]+$");
            Match type = typeRegex.Match(statusDto.Type);

            if (type.Success == false)
                validations.Add("Type Invalid", "Type must only contain letters");

            if (string.IsNullOrWhiteSpace(statusDto.Type))
                validations.Add("Type null", "Type is missing");

            if (statusDto.Type.Length > 30)
                validations.Add("Type length", "Maximun 30 Characters are allowed");

            if (validations.Count == 0)
            {
                var status = new Status
                {
                    Type = statusDto.Type,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value)
                };

                await _statusService.AddStatus(status);

                return StatusOutputDto.MapToEntity(status);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<StatusOutputDto> UpdateStatus(int statusId, StatusDto statusDto)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var status = await _statusService.GetStatusById(statusId);

            if (status == null)
                validations.Add("StatusId Invalid", "Status with this Id doesn't exist");

            Regex typeRegex = new Regex(@"^[a-zA-z\S]+$");
            Match type = typeRegex.Match(statusDto.Type);

            if (type.Success == false)
                validations.Add("Type Invalid", "Type must only contain letters");

            if (string.IsNullOrWhiteSpace(statusDto.Type))
                validations.Add("Type null", "Type is missing");

            if (statusDto.Type.Length > 30)
                validations.Add("Type length", "Maximun 30 Characters are allowed");

            if (validations.Count == 0)
            {
                status.Type = statusDto.Type;
                status.ChangedOn = DateTime.UtcNow;
                status.ChangedBy = int.Parse(_accessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);

                await _statusService.UpdateStatus(status);

                return StatusOutputDto.MapToEntity(status);
            }
            else
            {
                throw new CustomValidation(validations);
            }
        }

        public async Task<StatusOutputDto> DeleteStatus(int statusId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var status = await _statusService.GetStatusById(statusId);

            if (status != null)
            {
                await _statusService.DeleteStatus(status);

                return StatusOutputDto.MapToEntity(status);
            }
            else
            { 
                validations.Add("statusId Invalid", "Status with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }

        public async Task<StatusOutputDto> GetStatusById(int statusId)
        {
            Dictionary<string, string> validations = new Dictionary<string, string>();

            var status = await _statusService.GetStatusById(statusId);

            if (status != null)
            {
                return StatusOutputDto.MapToEntity(status);
            }
            else
            {
                validations.Add("statusId Invalid", "Status with this Id doesn't exist");

                throw new CustomValidation(validations);
            }
        }
    }
}
