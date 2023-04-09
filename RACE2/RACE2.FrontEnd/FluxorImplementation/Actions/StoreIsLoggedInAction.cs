using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEnd.FluxorImplementation.Actions
{
    public class StoreIsLoggedInAction
    {
        public bool IsLoggedIn { get; }
        public StoreIsLoggedInAction(bool isLoggedIn)
            => IsLoggedIn = isLoggedIn;
    }
}
