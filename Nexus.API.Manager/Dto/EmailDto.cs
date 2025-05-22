using Nexus.API.DataService.Models;

namespace Nexus.API.Manager.Dto
{
    class EmailDto
    {
        public string recipient { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public static EmailDto MapToEmail(User user, UserDetail userDetail) => new EmailDto
        {
            recipient = user.Email,
            subject = "Welcome to Nexus",
            body = $"<html><body>" +
                   $"<p>Dear {userDetail.LastName} {userDetail.FirstName},</p>" +
                   $"<p>Welcome to Nexus – your ultimate project management tool to boost your productivity and streamline your workflow!\r\n\r\nWe’re thrilled to have you on board. With Nexus, you can manage your tasks efficiently, collaborate with your team, and achieve your goals fa</p>" +
                   $"<img src='https://i.postimg.cc/x8WVdJPc/Screenshot-2025-03-12-141651.png' alt='Welcome Image' />" +
                   $"<p>Start exploring Nexus today and take your project management to the next level!</p>" +
                   $"<p>If you have any questions, feel free to reach out.</p>" +
                   $"<p>Happy Managing!</p>" +
                   $"<p>The Nexus Team</p>" +
                   $"</body></html>"
        };
    }
}