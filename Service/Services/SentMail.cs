using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace Service.Services;
public static class SentMail
{
    public static void SendMailOrder(IConfiguration configuration, string emailTo, string fullName, string orderId, DateTime orderDate, decimal total)
    {
        try
        {
            // Read HTML template from file
            string htmlTemplate = File.ReadAllText("wwwroot/static/index.html");

            // Replace placeholders in the HTML template
            string body = htmlTemplate
                .Replace("{{Fullname}}", fullName)
                .Replace("{{OrderId}}", orderId)
                .Replace("{{OrderDate}}", orderDate.ToString("dd/MM/yyyy HH:mm"))
                .Replace("{{Total}}", total.ToString("N0") + " VND");

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration["EmailUserName"]));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = "Xác nhận đơn hàng - Heartware";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(configuration["EmailHost"], 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(configuration["EmailUserName"], configuration["EmailPassword"]);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static void SentMailComplete(IConfiguration configuration, string emailTo, string fullName, string orderId,
        DateTime orderShippingComplete
        , decimal total, String address)
    {
        try
        {
            // Read HTML template from file
            string htmlTemplate = File.ReadAllText("wwwroot/static/complete-order.html");

            // Replace placeholders in the HTML template
            string body = htmlTemplate
                .Replace("{{Fullname}}", fullName)
                .Replace("{{OrderId}}", orderId)
                .Replace("{{ShippingComplete}}", orderShippingComplete.ToString("dd/MM/yyyy HH:mm"))
                .Replace("{{Total}}", total.ToString("N0") + " VND")
                .Replace("{{SubTotal}}", total.ToString("N0") + " VND");               

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration["EmailUserName"]));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = "Đơn hàng hoàn tất - Heartware";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(configuration["EmailHost"], 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(configuration["EmailUserName"], configuration["EmailPassword"]);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
