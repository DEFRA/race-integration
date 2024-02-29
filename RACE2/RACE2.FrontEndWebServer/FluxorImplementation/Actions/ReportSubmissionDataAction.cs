using RACE2.DataModel;
using RACE2.Dto;

namespace RACE2.FrontEndWebServer.FluxorImplementation.Actions
{
    public class ReportSubmissionDataAction
    {
        public ReportSubmissionDataDto ReportSubmissionData { get; }
        public ReportSubmissionDataAction(ReportSubmissionDataDto reportSubmissionData)
            => ReportSubmissionData = reportSubmissionData;
    }
}
