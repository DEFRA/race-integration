using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public interface IOpenXMLUtilitiesService
    {
        MemoryStream SearchAndReplace(Stream document, string reservoirName, string userName);
        Stream GenerateStreamFromString(string s);
        MemoryStream WordprocessingDocumentToStream(WordprocessingDocument wordDoc);
    }
}
