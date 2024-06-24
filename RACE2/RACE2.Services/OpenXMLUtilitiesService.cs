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
        public MemoryStream SearchAndReplace(Stream document, S12PrePopulationFields s12PrePopulationFields)
        {
            MemoryStream doc = new MemoryStream();
            document.CopyTo(doc);
            doc.Position = 0;
            string docText = null;

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(doc, true))
            {
                foreach (HeaderPart header in wordDoc.MainDocumentPart.HeaderParts)
                {
                    string headerText = null;
                    using (StreamReader sr = new StreamReader(header.GetStream()))
                    {
                        headerText = sr.ReadToEnd();
                    }
                    headerText = headerText.Replace("{Reservoir name}", s12PrePopulationFields.ReservoirName);
                    using (StreamWriter sw = new StreamWriter(header.GetStream(FileMode.Create)))
                    {
                        sw.Write(headerText);
                    }
                    //Save Header
                    header.Header.Save();
                }

                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                //input = input.Replace("&", "&amp;")
                //.Replace("'", "&apos;")
                //.Replace("\"", "&quot;")
                //.Replace(">", "&gt;")
                //.Replace("<", "&lt;");
                var tagList = new S12ReportTemplateTags().Tags;
                foreach (var tag in tagList)
                {
                    Regex tagVal = new Regex(tag, RegexOptions.NonBacktracking);
                    AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromMilliseconds(100));
                    switch (tag)
                    {
                        case "{Reservoir name}":                            
                            docText = tagVal.Replace(docText, s12PrePopulationFields.ReservoirName);
                            break;
                        case "{Reservoir nearest town}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.ReservoirNearestTown);
                            break;
                        case "{Reservoir grid reference}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.ReservoirGridRef);
                            break;
                        case "{Supervising engineer name}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.SupervisingEngineerName);
                            break;
                        case "{Supervising engineer company name}":
                            s12PrePopulationFields.SupervisingEngineerCompanyName = s12PrePopulationFields.SupervisingEngineerCompanyName.Replace("&", "&amp;");
                            docText = tagVal.Replace(docText, s12PrePopulationFields.SupervisingEngineerCompanyName);
                            break;
                        case "{Supervising engineer address}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.SupervisingEngineerAddress);
                            break;
                        case "{Supervising engineer email}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.SupervisingEngineerEmail);
                            break;
                        case "{Supervising engineer phone number}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.SupervisingEngineerPhoneNumber);
                            break;
                        case "{Undertaker name}":
                            s12PrePopulationFields.UndertakerName = s12PrePopulationFields.UndertakerName.Replace("&", "&amp;");
                            docText = tagVal.Replace(docText, s12PrePopulationFields.UndertakerName);                            
                             break;
                        case "{Undertaker address}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.UndertakerAddress);
                            break;
                        case "{Undertaker email}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.UndertakerEmail);
                            break;
                        case "{Undertaker phone number}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.UndertakerPhoneNumber);
                            break;
                        case "{Next inspection date}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.NextInspectionDate);
                            break;
                        case "{Last certification date}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.LastCertificationDate);
                            break;
                        case "{Last inspection date}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.LastInspectionDate);
                            break;
                        case "{Last inspecting engineer name}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.LastInspectingEngineerName);
                            break;
                        case "{Last inspecting engineer phone number}":
                            docText = tagVal.Replace(docText, s12PrePopulationFields.LastInspectingEngineerPhoneNumber);
                            break;
                        default:
                            break;
                    }
                }
                //Regex regexText = new Regex("Reservoir Name");
                //docText = regexText.Replace(docText, reservoirName);
                //Regex regexText1 = new Regex("Supervising Engineer Name");
                //docText = regexText1.Replace(docText, userName);
                Regex regexText = new Regex("Statement Date", RegexOptions.NonBacktracking);
                docText = regexText.Replace(docText, DateTime.Now.ToShortDateString());

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
