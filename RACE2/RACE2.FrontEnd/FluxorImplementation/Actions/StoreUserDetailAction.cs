using RACE2.DataModel;

namespace RACE2.FrontEnd.FluxorImplementation.Actions
{
    public class StoreUserDetailAction
    {
        public UserDetail? CurrentUserDetail { get; }
        public StoreUserDetailAction(UserDetail? userDetail)
            => CurrentUserDetail = userDetail;
    }
}
