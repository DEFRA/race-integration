using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.WebApi.Types
{
    [QueryType]
    public class RoleQueryResolver
    {
        private readonly ILogger<UserQueryResolver> _logger;

        public RoleQueryResolver(ILogger<UserQueryResolver> logger)
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
