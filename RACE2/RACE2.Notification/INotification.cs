using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Notification
{
    public interface INotification
    {
        Task SendMail(string Emailaddress);
        Task SendMail(string Emailaddress, string emailSubject, string emailContent);
        Task SendEmailTestWithPersonalisation(string Emailaddress);
    }
}
