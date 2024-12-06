namespace mongodb_net_api_crud.Models
{
    public class BooksDto
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public bool HasMoreItems { get; set; }
        public long TotalCount { get; set; }
        public IEnumerable<Book> Books { get; set; } = Enumerable.Empty<Book>();
    }
}
