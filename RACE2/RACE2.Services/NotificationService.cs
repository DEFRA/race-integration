using RACE2.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public class NotificationService: INotificationService
    {
        INotification _notificationService;
        public NotificationService(INotification notificationService) {
            _notificationService=notificationService;
        }

        public async Task SendMail(string Emailaddress)
        {
            await _notificationService.SendMail(Emailaddress);
        }

        public async Task SendForgotPasswordMail(string emailAddress, string fullName, string resetLink)
        {
            await _notificationService.SendForgotPasswordMail(emailAddress, fullName, resetLink);
        }

        public async Task SendEmailTestWithPersonalisation(string Emailaddress)
        {
            await _notificationService.SendEmailTestWithPersonalisation(Emailaddress);
        }

        public async Task SendConfirmationMailWithAttachment(byte[] file, string undertakerEmailaddress, string reservoirName)
        {
            await _notificationService.SendConfirmationMailWithAttachment(file,undertakerEmailaddress,reservoirName);
        }

        public async Task SendConfirmationMailtoSE(string SEEmailAddress, string reservoirName)
        {
            await _notificationService.SendConfirmationMailtoSE(SEEmailAddress, reservoirName);
        }
    }
}
