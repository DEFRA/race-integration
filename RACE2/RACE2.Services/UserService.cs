using RACE2.DataAccess.Repository;
using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RACE2.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<Userdetails>> GetUserdetails()
        {
            return await _userRepository.GetUserdetails();
        }

        public async Task<Userdetails> CreateUser(Userdetails newuser)
        {
           return await _userRepository.CreateUser(newuser);
        }
    }

}
