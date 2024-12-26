using api_portal.Data;
using api_portal.Dto;
using api_portal.Model;

namespace api_portal.Services
{
    public class BookService
    {
        private readonly ApplicationDBContext _dbContext;

        public BookService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> FindAll()
        {
            return [.. _dbContext.Books];
        }

        public Book FindById(int id)
        {
            return _dbContext.Books.SingleOrDefault(b => b.BookID == id);
        }

        public Book Save(BookRequest request)
        {
            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Genre = request.Genre,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                ISBN = request.ISBN,
                PublishedDate = request.PublishedDate
            };

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            return book;
        }

        public Book Update(int id, BookRequest request)
        {
            var book = FindById(id);

            if (book == null)
            {
                return null;
            }

            book.Title = request.Title;
            book.Author = request.Author;
            book.Genre = request.Genre;
            book.Price = request.Price;
            book.StockQuantity = request.StockQuantity;
            book.ISBN = request.ISBN;
            book.PublishedDate = request.PublishedDate;

            _dbContext.SaveChanges();

            return book;
        }

        public IEnumerable<Book> SearchAndFilter(string? searchTerm, string? genre, decimal? minPrice, decimal? maxPrice)
        {
            var query = _dbContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre == genre);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(b => b.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= maxPrice.Value);
            }

            return query.ToList();
        }
    }
}
