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
        public async Task<IEnumerable<Userdetail>> GetUserdetail()
        {
            return await _userRepository.GetUserdetail();
        }

        public async Task<Userdetail> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<Userdetail> GetUserByEmailID(string email)
        {
            return await _userRepository.GetUserByEmailID(email);
        }

        public async Task<Userdetail> GetUserWithRoles(string email)
        {
            return await _userRepository.GetUserWithRoles(email);
        }

        public async Task<Userdetail> CreateUser(Userdetail newuser)
        {
           return await _userRepository.CreateUser(newuser);
        }

        public async Task<Userdetail> ValidateUser(Userdetail loginuser)
        {
            return await _userRepository.ValidateUser(loginuser);
        }

    }

}
