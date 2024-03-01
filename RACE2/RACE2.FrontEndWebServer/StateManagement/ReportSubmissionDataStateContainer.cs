using RACE2.Dto;

namespace RACE2.FrontEndWebServer.StateManagement
{
    public class ReportSubmissionDataStateContainer
    {
        private ReportSubmissionDataDto savedReportSubmissionData;
        public ReportSubmissionDataDto Property
        {
            get => savedReportSubmissionData ?? new ReportSubmissionDataDto();
            set
            {
                savedReportSubmissionData = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
