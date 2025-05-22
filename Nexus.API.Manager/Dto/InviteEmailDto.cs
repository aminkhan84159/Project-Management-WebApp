using Nexus.API.DataService.Models;

namespace Nexus.API.Manager.Dto
{
    class InviteEmailDto
    {
        public string recipient { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public static InviteEmailDto MapToEmail(InviteDto inviteDto) => new InviteEmailDto
        {
            recipient = inviteDto.receiver,
            subject = "You’ve Been Invited to Join Nexus!",
            body = $"<html><body>" +
                   $"<p>Dear {inviteDto.receiver},</p>" +
                   $"<p>Your friend {inviteDto.sender} has invited you to join Nexus, the ultimate project management tool designed to enhance your productivity and collaboration.</p>" +
                   $"<p>🚀 Why Join Nexus?</p>" +
                   $"<p>✔️ Streamline your tasks and projects</p>" +
                   $"<p>✔️ Collaborate seamlessly with your team</p>" +
                   $"<p>✔️ Boost your efficiency with powerful tools</p>" +
                   $"<img src='https://i.postimg.cc/x8WVdJPc/Screenshot-2025-03-12-141651.png' alt='Welcome Image' />" +
                   $"<p>Click the link below to accept the invitation and start managing your projects effortlessly!\r\n\r\n</p>" +
                   $"<a href='http://localhost:4200'>Accept Invitation</a>" +
                   $"<p>We look forward to having you on board!</p>" +
                   $"<p>Best,</p>" +
                   $"<p>The Nexus Team</p>" +
                   $"</body></html>"
        };
    }
}
