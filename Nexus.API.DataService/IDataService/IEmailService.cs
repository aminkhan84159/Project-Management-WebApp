namespace Nexus.API.DataService.IDataService
{
    public interface IEmailService
    {
        Task SendEmail(string recepeint, string subject, string body);
    }
}
