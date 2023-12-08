using Notify.Client;
using Notify.Models;
using Notify.Models.Responses;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using Notify.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

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
        private readonly string InternalEmail = String.Empty;        
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
            InternalEmail = _config["InternalEmailTemplateId"];
        }

        public async Task SendForgotPasswordMail(string emailAddress, string fullName, string resetLink)
        {
            _logger.LogInformation("Sending Forgot mail to the user  ");
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
                _logger.LogError(ex, ex.Message);
            }

        }

        public async Task SendConfirmationMailWithAttachment(byte[] file, string undertakerEmailaddress, string reservoirName)
        {

            _logger.LogInformation("Sending confirmation mail to the undertaker");
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
                _logger.LogError(ex, ex.Message);
            }
        }


        public async Task SendConfirmationMailtoSE(string SEEmailAddress, string reservoirName)
        {
            _logger.LogInformation("Sending confirmation mail to the SE");
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
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task SendConfirmationMailtoRST(string RSTMailAddress, string reservoirName, byte[] file, string SEName, string UndertakerName)
        {
            _logger.LogInformation("Sending confirmation mail to the RST");
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
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task SendInternalMail(string EmailAddress, string reservoirName, string UndertakerEmail, string isSendtoUndertaker)
        {
            _logger.LogInformation("Sending Internal mail to the S12mailbox");
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
                {
                    { "name of reservoir", reservoirName },
                    { "owner/operator email",UndertakerEmail },
                    { "is send to undertaker", isSendtoUndertaker}
                   
                };

            try
            {
                var client = new NotificationClient(API_KEY);
                EmailNotificationResponse response = await client.SendEmailAsync(EmailAddress, StatementToRST, personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
