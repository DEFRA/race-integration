namespace RACE2.BackEnd.Types
{
    [QueryType]
    public static class Query
    {
        public static Book GetBook()
            => new Book("C# in depth.", new Author(1,"Jon Skeet"));
    }
}