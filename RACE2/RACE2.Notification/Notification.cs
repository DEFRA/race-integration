using Notify.Client;
using Notify.Models;
using Notify.Models.Responses;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using Notify.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace RACE2.Notification
{
    public class RaceNotification : INotification
    {
        private readonly ILogger<RaceNotification> _logger;
        private readonly IConfiguration _config;
        private readonly string API_KEY = String.Empty;
        private readonly string ForgetPasswordTemplateId = String.Empty;
        private readonly string ConfirmSubmissiontoOperator = String.Empty;
        private readonly string ConfirmSubmissiontoSE = String.Empty;
        private readonly string StatementToRST = String.Empty;
        public string reference = null;
        public string emailReplyToId = null;

        public RaceNotification(ILogger<RaceNotification> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            API_KEY = _config["NotifyAPIKEY"];
            ForgetPasswordTemplateId = _config["ForgetPasswordTemplateId"];
            ConfirmSubmissiontoOperator = _config["ConfirmSubmissiontoOperatorTemplateId"];
            ConfirmSubmissiontoSE = _config["ConfirmSubmissiontoSETemplateId"];
            StatementToRST = _config["StatementToRSTTemplateId"];
        }

        public async Task SendForgotPasswordMail(string emailAddress, string fullName, string resetLink)
        {
            //_logger.LogInformation("Sending Forgot mail to the user {fullName} ", fullName);
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
            {
                { "name of user", fullName },{"LINK",resetLink}
            };
            try
            {
                var client = new NotificationClient(API_KEY);
                EmailNotificationResponse response = await client.SendEmailAsync(emailAddress, ForgetPasswordTemplateId, personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                throw ex;
            }

        }

        public async Task SendConfirmationMailWithAttachment(byte[] file, string undertakerEmailaddress, string reservoirName)
        {
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
                {
                    { "name of reservoir", reservoirName },

                    { "link_to_file", NotificationClient.PrepareUpload(file)}

                };

            try
            {
                var client = new NotificationClient(API_KEY);
                EmailNotificationResponse response = await client.SendEmailAsync(undertakerEmailaddress, ConfirmSubmissiontoOperator, personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                throw ex;
            }
        }


        public async Task SendConfirmationMailtoSE(string SEEmailAddress, string reservoirName)
        {
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
                {
                    { "name of reservoir", reservoirName }


                };

            try
            {
                var client = new NotificationClient(API_KEY);
                EmailNotificationResponse response = await client.SendEmailAsync(SEEmailAddress, ConfirmSubmissiontoSE, personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                throw ex;
            }
        }

        public async Task SendConfirmationMailtoRST(string RSTMailAddress, string reservoirName, byte[] file, string SEName, string UndertakerName)
        {
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
                {
                    { "name of reservoir", reservoirName },
                    { "link_to_file", NotificationClient.PrepareUpload(file)},
                    { "name of supervising engineer", SEName},
                    { "name of undertaker", UndertakerName}

                };

            try
            {
                var client = new NotificationClient(API_KEY);
                EmailNotificationResponse response = await client.SendEmailAsync(RSTMailAddress, StatementToRST, personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                throw ex;
            }
        }
    }
}
