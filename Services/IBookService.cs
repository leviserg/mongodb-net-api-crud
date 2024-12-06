using mongodb_net_api_crud.Models;

namespace mongodb_net_api_crud.Services
{
    public interface IBookService
    {
        Task<BooksDto> GetBooksAsync(Filter filter);
        Task<Book> GetBookAsync(int id);
        Task<Book> CreateOrUpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);

    }
}
