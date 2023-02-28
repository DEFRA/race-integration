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
        public async Task<IEnumerable<UserDetail>> GetUserDetails()
        {
            return await _userRepository.GetUserDetails();
        }

        public async Task<UserDetail> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task<UserDetail> GetUserByEmailID(string email)
        {
            return await _userRepository.GetUserByEmailID(email);
        }

        public async Task<UserDetail> GetUserWithRoles(string email)
        {
            return await _userRepository.GetUserWithRoles(email);
        }

        public async Task<UserDetail> CreateUser(UserDetail newuser)
        {
           return await _userRepository.CreateUser(newuser);
        }

        public async Task<UserDetail> ValidateUser(UserDetail loginuser)
        {
            return await _userRepository.ValidateUser(loginuser);
        }

        public async Task<UserDetail> MatchUserWithEmailAndPasswordHash(string email, string passwordhash)
        {
            return await _userRepository.MatchUserWithEmailAndPasswordHash(email, passwordhash);
        }

        public async Task<UserDetail> UpdatePasswordHashForUser(int id, string passwordhash)
        {
            return await _userRepository.UpdatePasswordHashForUser(id, passwordhash);
        }
    }

}
