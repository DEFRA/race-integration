using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.BackEnd.Types
{
    [QueryType]
    public class RoleQueryResolver
    {
        private readonly ILogger<UserResolver> _logger;

        public RoleQueryResolver(ILogger<UserResolver> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<FeatureFunction>> GetFeaturePermissionForRole(IUserService _userService, int roleid)
        {
            var result = await _userService.GetFeaturePermissionForRole(roleid);
            return result;
        }
    }
}
