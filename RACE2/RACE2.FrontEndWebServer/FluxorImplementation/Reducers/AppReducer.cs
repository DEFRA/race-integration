using Fluxor;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;

namespace RACE2.FrontEndWebServer.FluxorImplementation.Reducers
{
    public static class AppReducer
    {

        [ReducerMethod]
        public static ReportSubmissionDataState ReduceReportSubmissionDataAction(ReportSubmissionDataState state, ReportSubmissionDataAction action)
             => new ReportSubmissionDataState(reportSubmissionData: action.ReportSubmissionData);      
       
    }
}
