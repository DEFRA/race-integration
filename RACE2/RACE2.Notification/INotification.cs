using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Notification
{
    public interface INotification
    {        
        Task SendForgotPasswordMail(string emailAddress, string fullName, string resetLink);
       
        Task SendConfirmationMailWithAttachment(byte[] file, string undertakerEmailaddress, string reservoirName);

        Task SendConfirmationMailtoSE(string SEEmailAddress, string reservoirName);

        Task SendConfirmationMailtoRST(string RSTMailAddress, string reservoirName, byte[] file, string SEName, string UndertakerName);

        Task SendInternalMail(string EmailAddress, string reservoirName, string UndertakerEmail, string isSendtoUndertaker);
    }
}
