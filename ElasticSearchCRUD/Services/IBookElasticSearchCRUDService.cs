using ElasticSearchCRUD.Models;

namespace ElasticSearchCRUD.Services
{
    public interface IBookElasticSearchCRUDService
    {
        Task<bool> CreateBookAsync(Book book);
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookAsync(int id);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);

        Task<List<Book>> FuzzySearchBooksAsync(string query);
    }

}
