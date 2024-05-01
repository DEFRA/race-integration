using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class IntegrationResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public string? Status { get; set; }

        public string? Reason { get; set; }

        public string ResponseData { get; set; }
    }

    public class IntegrationPayLoadModel
    {
        public string? uuid { get; set; }

        public string? email { get; set; }
    }

    public class PayloadModel
    {
        public IntegrationPayLoadModel engineer_reservoir_search { get; set; } = new IntegrationPayLoadModel();
    }


    public class S12StatementDetails
    {
        public string breach_of_act { get; set; }

        public string date_of_breach { get; set; }

        public string statement_type { get; set; }

        public string dir_under_section_126 { get; set; }

        public string recommend_inspection_sec_10 { get; set; }


    }

    public class DocumentDetails
    {
        public string name { get; set; }

        public byte[] content { get; set; }

        public string protective_marking { get; set; }

        public string document_date { get; set; }

        public class SubmitWrittenStatement
        {
            public string? uuid { get; set; }
            public string? reservoir_ref { get; set; }
            public string? engineer_ref { get; set; }

            public S12StatementDetails statement { get; set; } = new S12StatementDetails();

            public DocumentDetails document { get; set; } = new DocumentDetails();

        }

        public class SubmitS12Statement
        {
            // [JsonProperty(PropertyName = "submit-written-statement")]

            [BindProperty(Name = "submit-written-statement")]
            public SubmitWrittenStatement submitwrittenstatement { get; set; } = new SubmitWrittenStatement();
        }

    }
}
