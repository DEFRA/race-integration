using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RACE2.Services
{
    public interface IRACEIntegrationService
    {
        public Task<IntegrationResponseModel> GetEngineerReservoirByUUID(string uuid);

        public Task<IntegrationResponseModel> SubmitDocumentToBackend(AnnualSubmissionDocumentDetails submitS12Statement);

        public AnnualSubmissionDocumentDetails GenerateSubmitPayloadJSON(int submittedBy, string submissionreference, string notificationemailaddress, string reservoirbackendid,
            string reservoirreferencenumber, Stream filestream, int documentid, string uploadfilename, string blobstoragefilename, int engineerid, string backendprimaryref, string backendsecondref);
    }
}
