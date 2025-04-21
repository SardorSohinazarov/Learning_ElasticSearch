using Elasticsearch.Net;
using ElasticSearchCRUD.Models;
using Nest;

namespace ElasticSearchCRUD.Services
{
    public class BookElasticSearchCRUDService : IBookElasticSearchCRUDService
    {
        private readonly ElasticClient client;

        public BookElasticSearchCRUDService()
        {
            var settings = new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://elasticsearch:9200")))
                .DefaultIndex("books_index")
                .EnableApiVersioningHeader();

            client = new ElasticClient(settings);
        }

        // Shu id bilan yana qo'shilsa yangi qo'shvormaydi update qiladi
        public async Task<bool> CreateBookAsync(Book book)
        {
            var indexResponse = await client.IndexDocumentAsync(book);
            return indexResponse.IsValid;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var esResponse = await client.SearchAsync<Book>();

            return esResponse.Documents.ToList();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            var getResponse = await client.GetAsync<Book>(id);
            if (getResponse.IsValid)
                return getResponse.Source;

            return null;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            var updateResponse = await client.UpdateAsync<Book>(book.Id, u => u
                .Doc(book)
                .Refresh(Refresh.True)
            );

            return updateResponse.IsValid;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var deleteResponse = await client.DeleteAsync<Book>(id);
            return deleteResponse.IsValid;
        }

        public async Task<List<Book>> FuzzySearchBooksAsync(string query)
        {
            var searchResponse = await client.SearchAsync<Book>(s => s
                .Query(q => q
                    .MultiMatch(m => m
                    .Fields(f => f
                            .Field(p => p.Title)
                            .Field(p => p.Description)
                            .Field(p => p.Author)
                        )
                        .Query(query)
                        .Fuzziness(Fuzziness.Auto)
                    ))
            );

            if (searchResponse.IsValid)
                return searchResponse.Documents.ToList();

            return new List<Book>();
        }
    }

}
