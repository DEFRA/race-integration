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
        public Task<Userdetails> GetUserById(int id);
        public Task<Userdetails> GetUserByEmailID(string email);
        public Task<Userdetails> CreateUser(Userdetails newuser);
        public Task<Userdetails> ValidateUser(Userdetails loginuser);
        public Task<List<Userdetails>> GetUsersWithRoles();
    }
}
