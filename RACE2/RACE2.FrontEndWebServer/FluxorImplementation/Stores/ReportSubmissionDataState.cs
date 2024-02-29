using Fluxor;
using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEndWebServer.FluxorImplementation.Stores
{
    [FeatureState]
    public class ReportSubmissionDataState
    {
        public ReportSubmissionDataDto ReportSubmissionData { get; }

        private ReportSubmissionDataState() { } // Required for creating initial state

        public ReportSubmissionDataState(ReportSubmissionDataDto reportSubmissionData)
        {
            ReportSubmissionData = reportSubmissionData;
        }
    }
}

