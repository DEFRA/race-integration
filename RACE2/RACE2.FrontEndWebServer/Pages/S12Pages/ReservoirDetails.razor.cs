using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using RACE2.DataModel;
using RACE2.FrontEndWebServer.FluxorImplementation.Stores;
using RACE2.FrontEndWebServer.FluxorImplementation.Actions;
using RACE2.FrontEndWebServer.RACE2GraphQLSchema;
using Microsoft.AspNetCore.Components.Web;
using RACE2.Dto;
using Microsoft.AspNetCore.Components.Forms;
using RACE2.Services;
using System.IO;
using System.IO.Pipes;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Azure;

namespace RACE2.FrontEndWebServer.Pages.S12Pages
{
    public partial class ReservoirDetails
    {
        [Inject]
        private RACE2GraphQLClient client { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IState<CurrentReservoirState> CurrentReservoirState { get; set; } = default!;
        [Inject]
        public IState<CurrentUserDetailState> CurrentUserDetailState { get; set; } = default!;
        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;
        [Inject]
        public IJSRuntime jsRuntime { get; set; } = default!;
        [Inject]
        public IBlobStorageService blobStorageService { get; set; } = default!;

        public Reservoir CurrentReservoir { get; set; } = new Reservoir();
        public string ReservoirName { get; set; } = default!;
        private int UserId { get; set; } = 0;
        private string UserName { get; set; } = "Unknown";
        private UserDetail UserDetail { get; set; }

        protected override async void OnInitialized()
        {
            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserName = authState.User.Claims.ToList().FirstOrDefault(c => c.Type == "name").Value;
            //var userDetails = await client.GetUserByEmailID.ExecuteAsync(UserName);
            //UserId = userDetails!.Data!.UserByEmailID.Id;
            var userDetails = await client.GetUserByEmailID.ExecuteAsync(UserName);
            UserId = userDetails!.Data!.UserByEmailID.Id;
            UserDetail = new UserDetail()
            {
                UserName = UserName,
                Id = UserId,
                Email = userDetails!.Data!.UserByEmailID.Email
            };
            CurrentReservoir = CurrentReservoirState.Value.CurrentReservoir;
            base.OnInitialized();
        }

        public async void GoToNextPage()
        {
            bool forceLoad = false;
            NavigationManager.NavigateTo("/confirm-operator", forceLoad);
        }

        private void goback()
        {
            bool forceLoad = false;
            string pagelink = "/annual-statements";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }
        private async void DownloadReportTemplate()
        {
            var blobName = "S12ReportTemplate.docx";
            //var result1 = await client.WriteContentToBlob.ExecuteAsync(blobName, CurrentReservoir.PublicName);
            //var result2 = await client.DownloadBlobToLocalFile.ExecuteAsync(blobName, "d:\\temp\\" + blobName);
            Stream response = await blobStorageService.GetBlobFileStream(blobName);
            MemoryStream  procesedStream = SearchAndReplace(response);
            procesedStream.Position = 0;
            var streamRef = new DotNetStreamReference(stream: procesedStream);
            //var streamRef = new DotNetStreamReference(stream: response);
            await jsRuntime.InvokeVoidAsync("downloadFileFromStream", blobName, streamRef);
        }

        private async void UploadCompletedReport()
        {
            bool forceLoad = false;
            //string pagelink = "/upload-s12report";
            string pagelink = "/upload-multiple-s12reports";
            NavigationManager.NavigateTo(pagelink, forceLoad);
        }

        // To search and replace content in a document part.
        private MemoryStream SearchAndReplace(Stream document)
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

                Regex regexText = new Regex("Reservoir Name");
                docText = regexText.Replace(docText, CurrentReservoirState.Value.CurrentReservoir.PublicName);
                Regex regexText1 = new Regex("Supervisory Engineer");
                docText = regexText1.Replace(docText, UserDetail.UserName);
                Regex regexText2 = new Regex("Statement Date");
                docText = regexText2.Replace(docText, DateTime.Now.ToShortDateString());

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                return WordprocessingDocumentToStream(wordDoc);
            }
        }

        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private MemoryStream WordprocessingDocumentToStream(WordprocessingDocument wordDoc)
        {
            MemoryStream mem = new MemoryStream();

            using (var resultDoc = WordprocessingDocument.Create(mem, wordDoc.DocumentType))
            {

                // copy parts from source document to new document
                foreach (var part in wordDoc.Parts)
                {
                    OpenXmlPart targetPart = resultDoc.AddPart(part.OpenXmlPart, part.RelationshipId); // that's recursive :-)
                }

                //resultDoc.Dispose();
            }
            //resultDoc.Package.Close(); // must do this (or using), or the zip won't get created properly

            mem.Position = 0;

            return mem;            
        }
    }
}
