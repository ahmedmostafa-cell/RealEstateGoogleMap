
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        private readonly IConfiguration _configuration;

        public EmailSender(EmailConfiguration emailConfig , IConfiguration configuration)
        {
            _emailConfig = emailConfig;
            _configuration = configuration;
        }
      
        public void SendEmail(Message message, string id)
        {
            var emailMessage = CreateEmailMessage(message, id , "" , "");

            Send(emailMessage);
        }


        public async Task SendEmaillAsync(Message message ,string email, string subject, string messages)
        {
            var mailMessage = CreateEmaillMessage(message,email, subject, messages);
            await SendAsync(mailMessage);
        }

        public async Task SendEmailAsync(Message message, string id , string user , string email)
        {
            var mailMessage = CreateEmailMessage(message, id , user , email);

            await SendAsync(mailMessage);
        }

        public async Task SendEmailAsync2(Message message, string id, string user, string email)
        {
            var mailMessage = CreateEmailMessage2(message, id, user, email);

            await SendAsync(mailMessage);
        }

        public async Task SendEmaillConfirmAsync(Message message, string id, string user, string email)
        {
            var mailMessage = CreateEmailConfirmMessage(message, id, user, email);

            await SendAsync(mailMessage);
        }



        
        //public async Task SendEmailAsync2(Message message, string id, string user , TbRequest element)
        //{
        //    var mailMessage = CreateEmailMessage2(message, id, user , element);

        //    await SendAsync(mailMessage);
        //}
        //public async Task SendEmailAsync3(Message message, string id, string user, TbRequest element)
        //{
        //    var mailMessage = CreateEmailMessage3(message, id, user, element);

        //    await SendAsync(mailMessage);
        //}


        public async Task SendEmailAsyncToCustomer(Message message)
        {
            var mailMessage = CreateEmailMessageToCustomer(message);

            await SendAsync(mailMessage);
        }


        public async Task SendEmailAsyncToCustomerWithBookingDetails(Message message, Guid id)
        {
            var mailMessage = CreateEmailMessageToCustomerWithBookingDetails(message, id);

            await SendAsync(mailMessage);
        }
        public async Task sendEmailForResetPassword(Message message, Guid id)
        {
            var mailMessage = CreateEmailMessageToCustomerWithBookingDetails(message, id);

            await SendAsync(mailMessage);
        }


        public async Task SendEmailAsyncToCustomerNotConfirmedBooking(Message message, string Comment, string CustomerEmail)
        {
            var mailMessage = CreateEmailMessageToCustomerNotConfirmed(message, Comment, CustomerEmail);

            await SendAsync(mailMessage);
        }






        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    client.CheckCertificateRevocation = false;
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    client.CheckCertificateRevocation = false;

                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }





        private MimeMessage CreateEmailMessage(Message message, string id , string user , string email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string body = CreateBody(id, user , email);






            var bodyBuilder = new BodyBuilder { HtmlBody = CreateBody(id , user , email) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }




        private MimeMessage CreateEmailMessage2(Message message, string id, string user, string email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string body = CreateBody2(message, id, user, email);






            var bodyBuilder = new BodyBuilder { HtmlBody = CreateBody2(message, id, user, email) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }


        private MimeMessage CreateEmailConfirmMessage(Message message, string id, string user, string email)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string body = CreateBodyConfirm(id, user, email);






            var bodyBuilder = new BodyBuilder { HtmlBody = CreateBodyConfirm(id, user, email) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private MimeMessage CreateEmaillMessage(Message message,  string email,  string subject, string messages)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string body = CreateBody(messages, subject, email);






            var bodyBuilder = new BodyBuilder { HtmlBody = CreateBody(messages, subject, email) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }




        //private MimeMessage CreateEmailMessage2(Message message, string id, string user , TbRequest element)
        //{
        //    var emailMessage = new MimeMessage();
        //    emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
        //    emailMessage.To.AddRange(message.To);
        //    emailMessage.Subject = message.Subject;

        //    string body = CreateBody2(id, user , element);






        //    var bodyBuilder = new BodyBuilder { HtmlBody = CreateBody2(id, user , element) };

        //    if (message.Attachments != null && message.Attachments.Any())
        //    {
        //        byte[] fileBytes;
        //        foreach (var attachment in message.Attachments)
        //        {
        //            using (var ms = new MemoryStream())
        //            {
        //                attachment.CopyTo(ms);
        //                fileBytes = ms.ToArray();
        //            }

        //            bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
        //        }
        //    }

        //    emailMessage.Body = bodyBuilder.ToMessageBody();
        //    return emailMessage;
        //}


        //private MimeMessage CreateEmailMessage3(Message message, string id, string user, TbRequest element)
        //{
        //    var emailMessage = new MimeMessage();
        //    emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
        //    emailMessage.To.AddRange(message.To);
        //    emailMessage.Subject = message.Subject;

        //    string body = CreateBody3(id, user, element);






        //    var bodyBuilder = new BodyBuilder { HtmlBody = CreateBody3(id, user, element) };

        //    if (message.Attachments != null && message.Attachments.Any())
        //    {
        //        byte[] fileBytes;
        //        foreach (var attachment in message.Attachments)
        //        {
        //            using (var ms = new MemoryStream())
        //            {
        //                attachment.CopyTo(ms);
        //                fileBytes = ms.ToArray();
        //            }

        //            bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
        //        }
        //    }

        //    emailMessage.Body = bodyBuilder.ToMessageBody();
        //    return emailMessage;
        //}
        private MimeMessage CreateEmailMessageToCustomer(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, "Sender Email Address"));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string body = CreateBodyToAdmin();






            var bodyBuilder = new BodyBuilder { HtmlBody = CreateBodyToAdmin() };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }




        private MimeMessage CreateEmailMessageToCustomerWithBookingDetails(Message message, Guid id)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, "Sender Email Address"));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string body = CreateBodyToAdminWithBookingDetails(id);






            var bodyBuilder = new BodyBuilder { HtmlBody = CreateBodyToAdminWithBookingDetails(id) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private MimeMessage CreateEmailMessageToCustomerNotConfirmed(Message message, string Comment, string CustomerEmail)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, "Sender Email Address"));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            string body = CreateBodyToCustomerNotConfirmed(Comment, CustomerEmail);






            var bodyBuilder = new BodyBuilder { HtmlBody = CreateBodyToCustomerNotConfirmed(Comment, CustomerEmail) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }







        private string CreateBody(string id , string user , string email)
        {
            string Body = string.Empty;

            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Templates", "ForgotPassword.html")))
            {
                Body = reader.ReadToEnd();
            }
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            Body = Body.Replace("{{userName}}", email);
            Body = Body.Replace("{{link2}}", id);
            Body = Body.Replace("{{link1}}", user);



            Body = Body.Replace("InvoiceNUMBEeeR", "1gUVZ2AchQ8VNblfxskZZA_aSy6qYnbps");
            return Body;
        }

        private string CreateBody2(Message message, string id, string user, string email)
        {
            string Body = string.Empty;

            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Templates", "ForgotPassword2.html")))
            {
                Body = reader.ReadToEnd();
            }
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
           
            Body = Body.Replace("{{link1}}", message.Content);



            Body = Body.Replace("InvoiceNUMBEeeR", "1gUVZ2AchQ8VNblfxskZZA_aSy6qYnbps");
            return Body;
        }


        private string CreateBodyConfirm(string id, string user, string email)
        {
            string Body = string.Empty;

            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Templates", "ConfirmEmail.html")))
            {
                Body = reader.ReadToEnd();
            }
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            Body = Body.Replace("{{userName}}", email);
            Body = Body.Replace("{{link2}}", id);
            Body = Body.Replace("{{link1}}", user);



            Body = Body.Replace("InvoiceNUMBEeeR", "1gUVZ2AchQ8VNblfxskZZA_aSy6qYnbps");
            return Body;
        }
        //private string CreateBody2(string id, string user , TbRequest element)
        //{
        //    string Body = string.Empty;

        //    using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Templates", "htmlpage.html")))
        //    {
        //        Body = reader.ReadToEnd();
        //    }

        //    Body = Body.Replace("{{Subscription Type}}", element.SubscriptionType);
        //    Body = Body.Replace("{{Subscription Period}}", element.SubscriptionPeriod);
        //    Body = Body.Replace("{{Subscription Fee}}", (element.SubscriptionFee).ToString());
        //    Body = Body.Replace("{{PDF}}", (element.PdfFee).ToString());
        //    Body = Body.Replace("{{Trading View Username}}", element.UserName);
        //    Body = Body.Replace("{{E-mail}}", element.Email);
        //    Body = Body.Replace("{{Total Amount}}", (element.TotalProfit).ToString());
        //    Body = Body.Replace("{{Order ID}}", (element.RequestId).ToString());







        //    return Body;
        //}



        //private string CreateBody3(string id, string user, TbRequest element)
        //{
        //    string Body = string.Empty;

        //    using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Templates", "htmlpage1.html")))
        //    {
        //        Body = reader.ReadToEnd();
        //    }








        //    return Body;
        //}
        private string CreateBodyToAdmin()
        {
            string Body = string.Empty;

            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Templates", "SendEmailToCustomer.html")))
            {
                Body = reader.ReadToEnd();
            }



            return Body;
        }


        private string CreateBodyToAdminWithBookingDetails(Guid id)
        {
            string Body = string.Empty;

            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Templates", "SendEmailToCustomer.html")))
            {
                Body = reader.ReadToEnd();
            }

            Body = Body.Replace("InvoiceNUMBER", id.ToString());

            return Body;
        }




        private string CreateBodyToCustomerNotConfirmed(string Comment, string CustomerEmail)
        {
            string Body = string.Empty;

            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Templates", "SendEmailToCustomer NotConfirmed.html")))
            {
                Body = reader.ReadToEnd();
            }

            Body = Body.Replace("comment", Comment);

            return Body;
        }

      
    }
}
