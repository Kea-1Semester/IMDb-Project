namespace GraphQL.Types;

[QueryType]
public static class Query
{
    [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseFiltering]
    [UseSorting]
    public static List<Book> GetBook()
    {
        return new List<Book>
        {
            new Book("C# in depth.", new Author("Jon Skeet")),
            new Book("Introduction to Algorithms.", new Author("Thomas H. Cormen")),
            new Book("Design Patterns.", new Author("Erich Gamma")),
        };
    }
}
