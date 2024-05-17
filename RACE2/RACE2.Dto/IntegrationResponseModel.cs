﻿using Microsoft.AspNetCore.Mvc;
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


    public class submission
    {
        public int statusId { get; set; }
        public string reference { get; set; }
        public string submittedDate { get; set; }
        public int submittedBy { get; set; }
        public string type { get; set; }
        public string source { get; set; }
    }

    public class writtenStatement
    {
        public string type { get; set; }
        public string date { get; set; }

        public bool visualInspectionDirection { get; set; }
        public bool recommendInspectionS10 { get; set;}
        public string nextInspectionDate { get; set; }
        public string expectedNextStatementDate { get; set; }
        public string notificationEmailAddresses { get; set; }
    }

    public class reservoir
    {
        public string backEndId { get; set; }
        public string referenceNumber { get; set; }

     }

    public class engineer
    {
        public int id { get; set; }
        public string backEndPrimaryReference { get; set; }

        public string backEndSecondaryReference { get; set; }
    }
    public class breach
    {
    public string act { get; set; }
    public string date { get; set; }

    }
     public class document
    {
        public int id { get; set; }
        public string uploadFileName { get; set; }
        public string mimeType { get; set; }
        public string protectiveMarking { get; set; }
        public string templateType { get; set; }   
        public string templateVersion { get; set; }
        public string blobStorageFileName { get; set; }
        public byte[] content { get; set; }

        
    }

    public class AnnualSubmissionDocumentDetails
    {
       public submission submission { get; set; }
        public writtenStatement writtenStatement { get; set; }
        public reservoir reservoir { get; set; }
        public engineer engineer { get; set; }
         public breach breach { get; set; }

        public document document { get; set; }


    }
}
