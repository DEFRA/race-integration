using Notify.Client;
using Notify.Models;
using Notify.Models.Responses;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using Notify.Exceptions;

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

        //public string Emailaddress = "mahalakshmi.alagarsamy@capgemini.com" ;
        public string InvitationtemplateId = "8aac094b-9997-41c1-96fe-b35f415eea9f";
        public string CommentTemplateId = "0bbc0b12-aee0-4546-a20b-23e81489111c";
        public string ForgetPasswordTemplateId = "950e6a2b-8bbc-4374-9dc1-3a086d01bd3f";

        Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>();
            //{
            //    { "name", "someone" }
            //};
        public string reference = null;
        public string emailReplyToId = null;
 

        public async Task SendMail(string Emailaddress)
        {
            try
            {
                var client = new NotificationClient("race2frontend-34699fe1-7b6c-49b7-a562-358f403f75f1-921e2564-534c-4b0a-b705-b9ef5047dd45");
                EmailNotificationResponse response = await client.SendEmailAsync(Emailaddress, InvitationtemplateId, personalisation, reference, emailReplyToId);
            }
            catch (NotifyClientException ex)
            {
                throw ex;
            }
         
        }

        public async Task SendForgotPasswordMail(string emailAddress, string fullName, string resetLink)
        {
            Dictionary<String, dynamic> personalisation = new Dictionary<String, dynamic>
            {
                { "full_name", fullName },{"reset_link",resetLink}
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


    }
  
       
        
    
}
