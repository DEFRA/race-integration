﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
}
