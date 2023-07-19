using GraphQLFileUpload.Types;

namespace RACE2.BackEnd.Types.Query
{
    [QueryType]
    public class Query
    {
        public Book GetBook()
            => new Book("C# in depth.", new Author(1, "Jon Skeet"));
    }
}
