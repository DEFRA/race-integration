using System;
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
    }
}
