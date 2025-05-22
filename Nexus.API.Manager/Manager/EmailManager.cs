using Nexus.API.DataService.IDataService;
using Nexus.API.Manager.Dto;

namespace Nexus.API.Manager.Manager
{
    public class EmailManager
    {
        private readonly IEmailService _emailService;

        public EmailManager(
            IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmail(InviteDto inviteDto)
        {
            var invite = InviteEmailDto.MapToEmail(inviteDto);
            await _emailService.SendEmail(invite.recipient, invite.subject, invite.body);
        }
    }
}
