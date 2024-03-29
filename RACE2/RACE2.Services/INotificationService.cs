﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public interface INotificationService
    {
        Task SendMail(string Emailaddress);
        Task SendForgotPasswordMail(string emailAddress, string fullName, string resetLink);
        Task SendEmailTestWithPersonalisation(string Emailaddress);

        Task SendConfirmationMailWithAttachment(byte[] file, string undertakerEmailaddress, string reservoirName);

        Task SendConfirmationMailtoSE(string SEEmailAddress, string reservoirName);

        Task SendConfirmationMailtoRST(string RSTMailAddress, string reservoirName, byte[] file, string SEName, string UndertakerName);
    }
}
