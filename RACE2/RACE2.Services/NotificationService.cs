﻿using RACE2.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public class NotificationService: INotification
    {
        INotification _notificationService;
        public NotificationService(INotification notificationService) {
            _notificationService=notificationService;
        }


        public async Task SendForgotPasswordMail(string emailAddress, string fullName, string resetLink)
        {
            await _notificationService.SendForgotPasswordMail(emailAddress, fullName, resetLink);
        }


        public async Task SendConfirmationMailWithAttachment(byte[] file, string undertakerEmailaddress, string reservoirName)
        {
            await _notificationService.SendConfirmationMailWithAttachment(file,undertakerEmailaddress,reservoirName);
        }

        public async Task SendConfirmationMailtoSE(string SEEmailAddress, string reservoirName)
        {
            await _notificationService.SendConfirmationMailtoSE(SEEmailAddress, reservoirName);
        }

        public async Task SendConfirmationMailtoRST(string RSTMailAddress, string reservoirName, byte[] file, string SEName, string UndertakerName)
        {
            await _notificationService.SendConfirmationMailtoRST(RSTMailAddress, reservoirName,file,SEName,UndertakerName);
        }
        public async Task SendInternalMail(string EmailAddress, string reservoirName, string UndertakerEmail, string isSendtoUndertaker)
        {
            await _notificationService.SendInternalMail(EmailAddress, reservoirName, UndertakerEmail, isSendtoUndertaker);
        }
    }
}
