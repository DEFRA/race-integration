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

        public string GET_RECEIVED_TEXTS_URL = "v2/received-text-messages";
        public string GET_NOTIFICATION_URL = "v2/notifications/";
        public string GET_PDF_FOR_LETTER_URL = "v2/notifications/{0}/pdf";
        public string SEND_SMS_NOTIFICATION_URL = "v2/notifications/sms";
        public string SEND_EMAIL_NOTIFICATION_URL = "v2/notifications/email";
        public string SEND_LETTER_NOTIFICATION_URL = "v2/notifications/letter";
        public string GET_TEMPLATE_URL = "v2/template/";
        public string GET_ALL_NOTIFICATIONS_URL = "v2/notifications";
        public string GET_ALL_TEMPLATES_URL = "v2/templates";
        public string TYPE_PARAM = "?type=";
        public string VERSION_PARAM = "/version/";

        private readonly string API_KEY = String.Empty;
        private readonly string InvitationtemplateId = String.Empty;
        private readonly string CommentTemplateId = String.Empty;
        private readonly string ForgetPasswordTemplateId = String.Empty;
        private readonly string ConfirmSubmissiontoOperator = String.Empty;
        private readonly string ConfirmSubmissiontoSE = String.Empty;
        private readonly string StatementToRST = String.Empty;

        Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>();
        //{
        //    { "name", "someone" }
        //};
        public string reference = null;
        public string emailReplyToId = null;

        public RaceNotification(ILogger<RaceNotification> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            API_KEY = _config["NotifyAPIKEY"];
            //InvitationtemplateId = _config[""];
            //CommentTemplateId = _config[""];
            ForgetPasswordTemplateId = _config["ForgetPasswordTemplateId"];
            ConfirmSubmissiontoOperator = _config["ConfirmSubmissiontoOperatorTemplateId"];
            ConfirmSubmissiontoSE = _config["ConfirmSubmissiontoSETemplateId"];
            StatementToRST = _config["StatementToRSTTemplateId"];
        }


        public async Task SendMail(string Emailaddress)
        {
            try
            {
                var client = new NotificationClient(API_KEY);
                EmailNotificationResponse response = await client.SendEmailAsync(Emailaddress, InvitationtemplateId, personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                throw ex;
            }

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

        public async Task SendEmailTestWithPersonalisation(string Emailaddress)
        {
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
            {
                { "name", "Maha" },{"name of reservoir","Nottingam"}
            };
            var client = new NotificationClient("race2frontend-34699fe1-7b6c-49b7-a562-358f403f75f1-921e2564-534c-4b0a-b705-b9ef5047dd45");

            EmailNotificationResponse response =
                await client.SendEmailAsync(Emailaddress, CommentTemplateId, personalisation);
            // this.emailNotificationId = response.id;

            //Assert.IsNotNull(response);
            //Assert.AreEqual(response.content.body, TEST_EMAIL_BODY);
            // Assert.AreEqual(response.content.subject, TEST_EMAIL_SUBJECT);
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
