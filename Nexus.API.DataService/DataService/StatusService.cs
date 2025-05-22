using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Nexus.API.DataService.IDataService;
using Nexus.API.DataService.Models;

namespace Nexus.API.DataService.DataService
{
    public class StatusService : IStatusService
    {
        private readonly NexusContext _context;

        public StatusService(NexusContext context)
        {
            _context = context;
        }

        public async Task<List<Status>> GetAllStatus()
        {
            var res = await _context.Statuses.ToListAsync();

            return res;
        }

        public async Task<Status> AddStatus(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            return status;
        }

        public async Task<Status> UpdateStatus(Status status)
        {
            _context.Statuses.Update(status);
            await _context.SaveChangesAsync();

            return status;
        }

        public async Task<Status> DeleteStatus(Status status)
        {
            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return status;
        }

        public async Task<Status> GetStatusById(int? statusid)
        {
            var res = await _context.Statuses.FindAsync(statusid);

            return res;
        }
    }
}
