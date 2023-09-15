using DocumentFormat.OpenXml.Packaging;
using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public interface IOpenXMLUtilitiesService
    {
        MemoryStream SearchAndReplace(Stream document, S12PrePopulationFields s12PrePopulationFields);
        Stream GenerateStreamFromString(string s);
        MemoryStream WordprocessingDocumentToStream(WordprocessingDocument wordDoc);
    }
}
