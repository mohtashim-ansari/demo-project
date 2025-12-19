using System.Net;
using System.Net.Mail;

namespace dsr_web_api.Services;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task Send(string to, string subject, string body)
    {
        try
        {
            var smtp = _config.GetSection("Smtp");

            var client = new SmtpClient(smtp["Host"], int.Parse(smtp["Port"]))
            {
                EnableSsl = true,
                UseDefaultCredentials = false,   // 🔥 MUST ADD
                Credentials = new NetworkCredential(
                    smtp["User"],
                    smtp["Password"]
                )
            };

            var mail = new MailMessage
            {
                From = new MailAddress(smtp["User"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(to);

            await client.SendMailAsync(mail);
            Console.WriteLine($"Email successfully sent to {to}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email to {to}");
            Console.WriteLine(ex.Message);
        }
    }
}
