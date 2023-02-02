using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<Userdetails>> GetUserdetails();
        //   public Task<Userdetails> GetUser(int id);
        public Task<Userdetails> CreateUser(Userdetails newuser);
    }
}
