using Fluxor;
using RACE2.DataModel;

namespace RACE2.FrontEndWebapp.FluxorImplementation.Stores
{
    [FeatureState]
    public class CurrentUserDetailState
    {
        public UserDetail CurrentUserDetail { get; }

        private CurrentUserDetailState() { } // Required for creating initial state

        public CurrentUserDetailState(UserDetail currentUserDetail)
        {
            CurrentUserDetail = currentUserDetail;
        }
    }
}
