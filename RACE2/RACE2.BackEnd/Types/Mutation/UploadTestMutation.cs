using Path = System.IO.Path;
using System.IO;
using System.Text;
using RACE2.Dto;
using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.BackEnd.Types
{ 
   [MutationType]
    public class UploadTestMutation
    {
        //public async Task<Author> UploadDocument(int authorId, IFile file, CancellationToken cancellationToken)
        //{
        //    using var stream = File.Create(Path.Combine(ImageDirectory, $"{authorId}.png"));
        //    await file.CopyToAsync(stream, cancellationToken);
        //    return new Author(authorId, "Jon Skeet");
        //}

        public async Task<Author> UploadFileTestAsync(IReservoirService _reservoirService, string blobName,Author author)
        {
            string td = author.Name;
            string fileToWriteTo = @"d:\temp\test1.txt";
            byte[] test = Encoding.ASCII.GetBytes("C# Stream to File Example");

            using (MemoryStream memoryStream = new MemoryStream(test))
            {
                using Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create);

                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(streamToWriteTo);
            }
            return new Author(author.authourId, "Jon Skeet");
        }

        public async Task<Author> UploadFileTest1Async(IReservoirService _reservoirService, string blobName, UpdatedReservoirData updatedReservoir)
        {
            string td = updatedReservoir.PublicName;
            string fileToWriteTo = @"d:\temp\test1.txt";
            byte[] test = Encoding.ASCII.GetBytes("C# Stream to File Example");

            using (MemoryStream memoryStream = new MemoryStream(test))
            {
                using Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create);

                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(streamToWriteTo);
            }
            return new Author(updatedReservoir.Id, "Jon Skeet");
        }

    }
}

