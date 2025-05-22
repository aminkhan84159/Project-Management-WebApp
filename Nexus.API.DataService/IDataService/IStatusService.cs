using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.IDataService
{
    public interface IStatusService
    {
        Task<List<Status>> GetAllStatus();
        Task<Status> AddStatus(Status status);
        Task<Status> UpdateStatus(Status status);
        Task<Status> DeleteStatus(Status status);
        Task<Status> GetStatusById(int? statusId);
    }
}
