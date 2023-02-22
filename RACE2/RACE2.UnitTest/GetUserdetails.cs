using Microsoft.Extensions.Configuration;
using RACE2.DataAccess.Repository;
using System.IO;

namespace RACE2.UnitTest
{
    public class GetUserDetail
    {
        public GetUserDetail()
        {
        }
        [Fact]
        public void GetFeatureFunctionTest()
        {
            //var config = InitConfiguration();
            //var connectionString = config.GetConnectionString("DefaultConnection");
            //UserRepository repo = new UserRepository(config);
            ////var details = repo.GetFeatureFunctions(config);
            //var details = repo.GetUserDetail();
            //Assert.NotNull(details);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }
    }
}