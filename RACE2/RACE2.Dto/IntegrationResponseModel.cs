using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class IntegrationResponseModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public string? Status { get; set; }

        public string? Reason { get; set; }

        public object? ResponseData { get; set; } 
    }

    public class IntegrationPayLoadModel
    {
        public string? uuid { get; set; }

        public string? email { get; set; }
    }
}
