using MongoDB.Driver;
using mongodb_net_api_crud.Models;

namespace mongodb_net_api_crud.Services
{
    public class BookService : IBookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IMongoDbStoreSettings settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>(settings.CollectionName);
        }

        public async Task<Book> CreateOrUpdateBookAsync(Book book)
        {
            if(book.Id == null)
            {
                return await StoreIfNotExists(book);
            }

            var filter = Builders<Book>.Filter.Eq(b => b.Id, book.Id);

            var selectedBook = await _books.FindAsync<Book>(filter);

            if (selectedBook == null) { 
                return await StoreIfNotExists(book);
            }
            else
            {
                var update = Builders<Book>.Update
                    .Set(b => b.Name, book.Name)
                    .Set(b => b.Authors, book.Authors)
                    .Set(b => b.Issued, book.Issued);

                var updatedBook = await _books.FindOneAndUpdateAsync(
                    filter,
                    update,
                    new FindOneAndUpdateOptions<Book>
                    {
                        ReturnDocument = ReturnDocument.After
                    });

                return updatedBook;
            }


        }

        public async Task DeleteBookAsync(int id)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, id);

            var deleteResult = await _books.DeleteOneAsync(filter);

            if (deleteResult.DeletedCount == 0)
            {
                throw new KeyNotFoundException($"No book found with ID {id} to delete.");
            }
        }

        public async Task<Book> GetBookAsync(int id)
        {
            var book = await _books.FindAsync<Book>(b => b.Id == id);
            return book.FirstOrDefault();
        }

        public async Task<BooksDto> GetBooksAsync(Filter filter)
        {

            var sortDefinition = filter.OrderDesc
                ? Builders<Book>.Sort.Descending(filter.SortBy)
                : Builders<Book>.Sort.Ascending(filter.SortBy);


            long totalCount = await _books.CountDocumentsAsync(FilterDefinition<Book>.Empty);

            int pageSize = filter.PageSize;
            int pageNo = filter.PageNo;
            int offset = (pageNo - 1) * pageSize;
            bool hasMoreItems = totalCount > (offset + pageSize);

            var books = await _books.Find(FilterDefinition<Book>.Empty)
                .Sort(sortDefinition)
                .Skip(offset)
                .Limit(pageSize)
                .ToListAsync();

            return new BooksDto
            {
                TotalCount = totalCount,
                Books = books,
                PageSize = pageSize,
                PageNo = pageNo,
                HasMoreItems = hasMoreItems
            };

        }

        private async Task<Book> StoreIfNotExists(Book book)
        {
            try
            {
                var bookWithMaxId = await _books.Find(FilterDefinition<Book>.Empty).SortByDescending(x => x.Id).Limit(1).FirstOrDefaultAsync();
                int maxId = bookWithMaxId?.Id ?? 0;
                int newId = maxId + 1;
                var bookToStore = new Book
                {
                    Id = newId,
                    Name = book.Name,
                    Issued = book.Issued,
                    Authors = book.Authors
                };
                await _books.InsertOneAsync(bookToStore);
                return bookToStore;
            }
            catch (Exception ex) { 
                throw new Exception($"Could not save book with new id");
            }

        }
    }
}
