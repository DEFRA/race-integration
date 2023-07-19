using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.BackEnd.Types
{
    [QueryType]
    public class RoleQueryResolver
    {
        private readonly ILogger<RoleQueryResolver> _logger;
        private readonly IConfiguration _configuration;
        public RoleQueryResolver(ILogger<RoleQueryResolver> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<FeatureFunction>> GetFeaturePermissionForRole([Service] IUserService _userService, int roleid)
        {
            var result = await _userService.GetFeaturePermissionForRole(roleid);
            return result;
        }
    }
}
