using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RACE2.DataAccess.Repository
{
    public interface IRACEIntegrationRepository
    {
        public Task<IntegrationResponseModel> GetEngineerReservoirByUUID(string uuid);

        public Task<IntegrationResponseModel> SubmitDocumentToBackend(AnnualSubmissionDocumentDetails submitWrittenStatement);

        public AnnualSubmissionDocumentDetails GenerateSubmitPayloadJSON(int submittedBy, string submissionreference,string notificationemailaddress,string reservoirbackendid,
            string reservoirreferencenumber, Stream filestream,int documentid,string uploadfilename,string blobstoragefilename, int engineerid,string backendprimaryref,string backendsecondref);
    }

}
