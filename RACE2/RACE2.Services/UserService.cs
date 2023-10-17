using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using RACE2.DataAccess.Repository;
using RACE2.DataModel;
using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
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
            try
            {
                return await _userRepository.GetUserDetails();
            }
            catch (Exception ex)
            {
                return new List<UserDetail>();
            }
            
        }

        public async Task<UserDetail> GetUserById(int id)
        {
            try
            {
                return await _userRepository.GetUserById(id);
            }
            catch (Exception ex)
            {
                return new UserDetail();
            }
            
        }

        public async Task<UserSpecificDto> GetUserByEmailID(string email)
        {
            try
            {
                return await _userRepository.GetUserByEmailID(email);
            }
            catch (Exception ex)
            {
                return new UserSpecificDto();
            }
           
        }

        public async Task<UserSpecificDto> GetUserWithRoles(string email)
        {
            try
            {
                return await _userRepository.GetUserWithRoles(email);
            }
            catch (Exception ex)
            {
                return new UserSpecificDto();
            }
            
        }

        //public async Task<UserDetail> CreateUser(UserDetail newuser)
        //{
        //    try
        //    {
        //        return await _userRepository.CreateUser(newuser);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new UserDetail();
        //    }
            
        //}

        //public async Task<UserDetail> ValidateUser(UserDetail loginuser)
        //{
        //    try
        //    {
        //        return await _userRepository.ValidateUser(loginuser);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new UserDetail();
        //    }
            
        //}

        public async Task<UserDetail> MatchUserWithEmailAndPasswordHash(string email, string passwordhash)
        {
            try
            {
                return await _userRepository.MatchUserWithEmailAndPasswordHash(email, passwordhash);
            }
            catch (Exception ex)
            {
                return new UserDetail();
            }
            
        }

        public async Task<UserDetail> UpdatePasswordHashForUser(int id, string passwordhash)
        {
            try
            {
                return await _userRepository.UpdatePasswordHashForUser(id, passwordhash);
            }
            catch (Exception ex)
            {
                return new UserDetail();
            }
            
        }
       

        public async Task<IEnumerable<FeatureFunction>> GetFeaturePermissionForRole(int roleid)
        {
            try
            {
                return await _userRepository.GetFeaturePermissionForRole(roleid);
            }
            catch (Exception ex)
            {
                return new List<FeatureFunction>();
            }
            
        }

        public async Task<OrganisationDTO> GetOrganisationAddressbyId(int userId)
        {
            try
            {
                return await _userRepository.GetOrganisationAddressbyId(userId);
            }
            catch (Exception ex)
            {
                return new OrganisationDTO();
            }
            
        }

        public async Task<int> UpdateFirstTimeUserLogin(string email)
        {
            try
            {
                return await _userRepository.UpdateFirstTimeUserLogin(email);
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }

    }

}
