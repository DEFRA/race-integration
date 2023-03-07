using Fluxor;
using Microsoft.AspNetCore.Components;
using RACE2.DataModel;
using RACE2.FrontEnd.RACE2GraphQLSchema;
using System.Net.Http.Json;
using System.Text.Json;

namespace RACE2.FrontEnd.Features.CurrentUserDetail.Store
{
    [FeatureState]
    public record CurrentUserDetailState
    {
        public UserDetail CurrentUserDetail { get; init; }
        private CurrentUserDetailState() { } // Required for creating initial state

        public CurrentUserDetailState(UserDetail userDetail)
        {
            CurrentUserDetail = userDetail;
        }
    }

    public static class CurrentUserDetailReducers
    {
        [ReducerMethod]
        public static CurrentUserDetailState ReduceStoreUserDetailAction(CurrentUserDetailState state, CurrentUserDetailSaveAction action)
             => state with
             {
                 CurrentUserDetail = action.CurrentUserDetail
             };
    }

    public class CurrentUserDetailSaveAction
    {
        public UserDetail CurrentUserDetail { get; }
        public CurrentUserDetailSaveAction(UserDetail currentUserDetail)
        {
            CurrentUserDetail = currentUserDetail;
        }
    }

    public static class Reducers
    {
        [ReducerMethod]
        public static CurrentUserDetailState ReduceIncrementCounterAction(CurrentUserDetailState state, CurrentUserDetailSaveAction action) =>
          new CurrentUserDetailState(userDetail: action.CurrentUserDetail);
    }
}
