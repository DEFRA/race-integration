using GraphQLFileUpload.Types;
using RACE2.DataModel;
using RACE2.Services;

namespace RACE2.BackEnd.Types.Query
{
    [QueryType]
    public class Query
    {
        private readonly ILogger<Query> _logger;
        private readonly IConfiguration _configuration;

        public Query(ILogger<Query> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public Book GetBook()
            => new Book("C# in depth.", new Author(1, "Jon Skeet"));        
    }
}
