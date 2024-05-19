using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RACE2.DataAccess.Repository;
using RACE2.Dto;
using System.Data;

namespace RACE2.BackendAPIIntegration.Services
{
    public class FileRepository :IFileRepository
    {

        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;
        public FileRepository(IConfiguration configuration, ILogger<UserRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection("Server=tcp:pocracinfss1402.database.windows.net,1433;Initial Catalog=pocracinfdb1402;Persist Security Info=False;User ID=race2admin;Password=Race2Password123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");//(_configuration["SqlConnectionString"]);
            }
        }


        public async Task PostFileAsync(IFormFile fileData, FileType fileType)
        {
            //try
            //{
            //    var fileDetails = new DocumentDetails()
            //    {
            //        name = fileData.FileName,
            //        document_date = DateTime.Now.ToShortDateString(),
            //         protective_marking = "Official"
            //        //ID = 0,
            //        //FileName = fileData.FileName,
            //        //FileType = fileType,
            //    };

            //    using (var stream = new MemoryStream())
            //    {
            //        fileData.CopyTo(stream);
            //        fileDetails.content = stream.ToArray();
            //    }

            //    using(var conn = Connection)
            //    {

            //        var query = "INSERT INTO FileDetails (FileName,FileData) VALUES (@name,@content)"
            //   + "SELECT CAST(SCOPE_IDENTITY() as int)";

            //        var parameters = new DynamicParameters();
            //        parameters.Add("name", fileDetails.name, DbType.String);
            //        parameters.Add("content", fileDetails.content, DbType.Binary);
            //       // parameters.Add("type", 1, DbType.Int32);

            //        int id = await conn.ExecuteScalarAsync<int>(query, fileDetails);

            //    }

                
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

    }
}
