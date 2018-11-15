using System;
using System.Net;
using System.Net.Mail;

namespace Jubin.Utils.Email
{
    public class EmailClient
    {
        public static bool SendEmail(string from, string fromName, string to,
            string smtpUser, string smtpPass, string subject,
            string body)
        {
            // Replace sender@example.com with your "From" address. 
            // This address must be verified with Amazon SES.


            // Replace recipient@example.com with a "To" address. If your account 
            // is still in the sandbox, this address must be verified.


            // Replace smtp_username with your Amazon SES SMTP user name.


            // Replace smtp_password with your Amazon SES SMTP user name.


            // (Optional) the name of a configuration set to use for this message.
            // If you comment out this line, you also need to remove or comment out
            // the "X-SES-CONFIGURATION-SET" header below.
            //const String CONFIGSET = "ConfigSet";

            // If you're using Amazon SES in a region other than US West (Oregon), 
            // replace email-smtp.us-west-2.amazonaws.com with the Amazon SES SMTP  
            // endpoint in the appropriate Region.
            const String HOST = "email-smtp.us-west-2.amazonaws.com";

            // The port you will connect to on the Amazon SES SMTP endpoint. We
            // are choosing port 587 because we will use STARTTLS to encrypt
            // the connection.
            const int PORT = 587;

            // Create and build a new MailMessage object
            MailMessage message = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(from, fromName)
            };
            message.To.Add(new MailAddress(to));
            message.Subject = subject;

            //message.Body = body;

            // Comment or delete the next line if you are not using a configuration set
            //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

            // Create and configure a new SmtpClient
            SmtpClient client =
                new SmtpClient(HOST, PORT);
            // Pass SMTP credentials
            client.Credentials =
                new NetworkCredential(smtpUser, smtpPass);
            // Enable SSL encryption
            client.EnableSsl = true;

            // Send the email. 
            try
            {
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
            }
            return false;

        }
    }
}
