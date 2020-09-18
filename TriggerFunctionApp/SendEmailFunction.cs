using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;

namespace TriggerFunctionApp
{
    public static class SendEmailFunction
    {
        [FunctionName("SendEmailFunction")]
        public static void Run([ServiceBusTrigger("asb-topic", "ASBT-Trigger-subscription-local", Connection = "ConnectionString")] Message message,
            [SendGrid(ApiKey = "CustomSendGridKeyAppSettingName")] out SendGridMessage sendGridMessage, ILogger log)
        {
            var emailObject = JsonConvert.DeserializeObject<OutgoingEmail>(Encoding.UTF8.GetString(message.Body));

            sendGridMessage = new SendGridMessage();
            sendGridMessage.AddTo(emailObject.ToEmail);
            sendGridMessage.AddContent("text/html", emailObject.Content);
            sendGridMessage.SetFrom(new EmailAddress(emailObject.FromEmail));
            sendGridMessage.SetSubject(emailObject.Subject);
        }

        public class OutgoingEmail
        {
            public string ToEmail { get; set; }
            public string FromEmail { get; set; }
            public string Subject { get; set; }
            public string Content { get; set; }
        }
    }
}
