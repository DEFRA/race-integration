using Fluxor;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;

namespace RACE2.FrontEndWebServer.FluxorImplementation.Reducers
{
    public static class AppReducer
    {
        [ReducerMethod]
        public static CurrentUserDetailState ReduceStoreUserDetailAction(CurrentUserDetailState state, StoreUserDetailAction action)
             => new CurrentUserDetailState(currentUserDetail:action.CurrentUserDetail);

        [ReducerMethod]
        public static CurrentReservoirState ReduceStoreReservoirAction(CurrentReservoirState state, StoreReservoirAction action)
             => new CurrentReservoirState(currentReservoir: action.CurrentReservoir);

        //[ReducerMethod]
        //public static AppStore ReduceStoreNewReservoirAction(AppStore state, StoreNewReservoirAction action)
        //     => state with { 
        //         IsLoading = false, 
        //         NewReservoirDetails = action.NewReservoirDetails 
        //     };

        [ReducerMethod]
        public static AppStore ReduceStoreIsLoggedInAction(AppStore state, StoreIsLoggedInAction action)
             => state with { 
                 IsLoggedIn = action.IsLoggedIn 
             };

    }
}
