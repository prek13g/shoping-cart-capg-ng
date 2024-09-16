using System.Net.Mail;
using System.Net;
using System.Text;
namespace Shoping_cart.services
{
    public class EmailSending
    {
        public void SendEmail(string toEmail, string username)
        {
            // Set up SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("prekshadixit6@gmail.com", "bblq wwdg brri pgwc");

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("prekshadixit6@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Regarding shopping_cart application";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>User Registered</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat($"<p>Dear {username},</p>");
            mailBody.AppendFormat("<p>Thank you For Registering account</p>");
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }
    }
}

