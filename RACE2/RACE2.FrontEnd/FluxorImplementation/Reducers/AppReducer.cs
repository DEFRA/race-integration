using Fluxor;
using RACE2.FrontEnd.FluxorImplementation.Actions;
using RACE2.FrontEnd.FluxorImplementation.Stores;

namespace RACE2.FrontEnd.FluxorImplementation.Reducers
{
    public static class AppReducer
    {
        [ReducerMethod]
        public static CurrentUserDetailState ReduceStoreUserDetailAction(CurrentUserDetailState state, StoreUserDetailAction action)
             => new CurrentUserDetailState(currentUserDetail:action.CurrentUserDetail);

        [ReducerMethod]
        public static CurrentReservoirState ReduceStoreReservoirAction(CurrentReservoirState state, StoreReservoirAction action)
             => new CurrentReservoirState(currentReservoir: action.CurrentReservoir);
        
        [ReducerMethod]
        public static UserReservoirsState ReduceStoreUserReservoirsAction(UserReservoirsState state, StoreUserReservoirsAction action)
             => new UserReservoirsState(userReservoirs: action.UserReservoirs);

        [ReducerMethod]
        public static NewReservoirState ReduceStoreNewReservoirAction(NewReservoirState state, StoreNewReservoirAction action)
             => new NewReservoirState(newReservoir: action.NewReservoir);

        [ReducerMethod]
        public static AppStore ReduceStoreIsLoggedInAction(AppStore state, StoreIsLoggedInAction action)
             => state with { 
                 IsLoggedIn = action.IsLoggedIn 
             };

    }
}
