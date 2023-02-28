using Fluxor;
using RACE2.FrontEndWeb.FluxorImplementation.Actions;
using RACE2.FrontEndWeb.FluxorImplementation.Stores;

namespace RACE2.FrontEndWeb.FluxorImplementation.Reducers
{
    public static class AppReducer
    {
        [ReducerMethod]
        public static AppStore ReduceStoreReservoirAction(AppStore state, StoreReservoirAction action)
         => state with { IsLoading = false, CurrentReservoir = action.CurrentReservoir };

        [ReducerMethod]
        public static AppStore ReduceStoreUserDetailAction(AppStore state, StoreUserDetailAction action)
         => state with { IsLoading = false, CurrentUserDetail = action.CurrentUserDetail };

        [ReducerMethod]
        public static AppStore ReduceStoreNewReservoirAction(AppStore state, StoreNewReservoirAction action)
         => state with { IsLoading = false, NewReservoirDetails = action.NewReservoirDetails };

        [ReducerMethod]
        public static AppStore ReduceStoreIsLoggedInAction(AppStore state, StoreIsLoggedInAction action)
         => state with { IsLoggedIn = action.IsLoggedIn };

        [ReducerMethod]
        public static AppStore ReduceStoreLastPasswordEnteredAction(AppStore state, StoreLastPasswordEnteredAction action)
        => state with { LastPasswordEntered = action.LastPasswordEntered };
    }
}
