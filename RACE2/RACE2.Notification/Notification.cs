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
    public class RaceNotification: INotification
    {
      //  public string NOTIFY_API_URL = Environment.GetEnvironmentVariable("NOTIFY_API_URL");
        public string API_KEY = "race2frontend-34699fe1-7b6c-49b7-a562-358f403f75f1-921e2564-534c-4b0a-b705-b9ef5047dd45";
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
        private readonly ILogger<RaceNotification> _logger;
        private readonly IConfiguration _configuration;

        //public string Emailaddress = "mahalakshmi.alagarsamy@capgemini.com" ;
        public string InvitationtemplateId = "8aac094b-9997-41c1-96fe-b35f415eea9f";
        public string CommentTemplateId = "0bbc0b12-aee0-4546-a20b-23e81489111c";
        public string ForgetPasswordTemplateId = "950e6a2b-8bbc-4374-9dc1-3a086d01bd3f";
        public string ConfirmSubmissiontoOperatorTemplateId = "76c2562e-8bb6-4f98-830d-a76b30c7d4e5";
        public string ConfirmSubmissiontoSETemplateId = "4f801102-36f4-4ce9-b59c-baf5c2488bcc";
        public string StatementToRSTTemplateId = "f4f9218f-9e7e-4aaa-84ea-3a1bd61afef5";

        Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>();
            //{
            //    { "name", "someone" }
            //};
        public string reference = null;
        public string emailReplyToId = null;

        public RaceNotification(ILogger<RaceNotification> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
           
        }



        public async Task SendForgotPasswordMail(string emailAddress, string fullName, string resetLink)
        {
            _logger.LogInformation("Sending Forgot mail to the user {fullName} ", fullName);
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
            {
                { "name of user", fullName },{"LINK",resetLink}
            };
            try
            {
                var client = new NotificationClient(_configuration["NotifyAPIKEY"]);
                EmailNotificationResponse response = await client.SendEmailAsync(emailAddress, _configuration["ForgetPasswordTemplateId"], personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                _logger.LogError(ex,ex.Message);
                
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
                var client = new NotificationClient(_configuration["NotifyAPIKEY"]);
                EmailNotificationResponse response = await client.SendEmailAsync(undertakerEmailaddress, _configuration["ConfirmSubmissiontoOperatorTemplateId"], personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                _logger.LogError(ex, ex.Message);
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
                var client = new NotificationClient(_configuration["NotifyAPIKEY"]);
                EmailNotificationResponse response = await client.SendEmailAsync(SEEmailAddress, _configuration["ConfirmSubmissiontoSETemplateId"], personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task SendConfirmationMailtoRST(string RSTMailAddress, string reservoirName, byte[] file, string SEName, string UndertakerName)
        {
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
                {
                    { "name of reservoir", reservoirName },
                    { "link_to_file", NotificationClient.PrepareUpload(file)},
                {"name of supervising engineer",SEName}, 
                {"name of undertaker",UndertakerName}

                };

            try
            {
                var client = new NotificationClient(_configuration["NotifyAPIKEY"]);
                EmailNotificationResponse response = await client.SendEmailAsync(RSTMailAddress, _configuration["StatementToRSTTemplateId"], personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }


    }
  
       
        
    
}
