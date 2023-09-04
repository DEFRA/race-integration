using DocumentFormat.OpenXml.Packaging;
using RACE2.DataModel;
using RACE2.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RACE2.Services
{
    public class OpenXMLUtilitiesService: IOpenXMLUtilitiesService
    {

        // To search and replace content in a document part.
        public MemoryStream SearchAndReplace(Stream document,string reservoirName,string userName)
        {
            MemoryStream doc = new MemoryStream();
            document.CopyTo(doc);
            doc.Position = 0;
            string docText = null;

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(doc, true))
            {
                //string docText = null;

                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }
                //var tagList = new S12ReportTemplateTags().Tags;
                //foreach (var tag in tagList)
                //{
                //    Regex regexText = new Regex(tag);
                //    docText = regexText.Replace(docText, reservoirName);
                //}
                Regex regexText = new Regex("Reservoir Name");
                docText = regexText.Replace(docText, reservoirName);
                Regex regexText1 = new Regex("Supervising Engineer Name");
                docText = regexText1.Replace(docText, userName);
                Regex regexText2 = new Regex("Statement Date");
                docText = regexText2.Replace(docText, DateTime.Now.ToShortDateString());

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                return WordprocessingDocumentToStream(wordDoc);
            }
        }

        public Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public MemoryStream WordprocessingDocumentToStream(WordprocessingDocument wordDoc)
        {
            MemoryStream mem = new MemoryStream();

            using (var resultDoc = WordprocessingDocument.Create(mem, wordDoc.DocumentType))
            {

                // copy parts from source document to new document
                foreach (var part in wordDoc.Parts)
                {
                    OpenXmlPart targetPart = resultDoc.AddPart(part.OpenXmlPart, part.RelationshipId); // that's recursive :-)
                }
            }

            mem.Position = 0;

            return mem;
        }       
    }
}
