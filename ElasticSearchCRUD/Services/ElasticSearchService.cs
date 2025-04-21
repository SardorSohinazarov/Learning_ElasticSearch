using ElasticSearchCRUD.Models;
using Nest;

namespace ElasticSearchCRUD.Services
{
    public class ElasticSearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchService(IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration["ElasticSearch:Url"]))
                .DefaultIndex(configuration["ElasticSearch:Index"]);

            _elasticClient = new ElasticClient(settings);
        }

        public async Task CreateIndexAsync()
        {
            var exists = await _elasticClient.Indices.ExistsAsync("products");
            if (!exists.Exists)
            {
                var createIndexResponse = await _elasticClient.Indices.CreateAsync("products", c => c
                    .Map(m => m
                        .AutoMap()
                    )
                );
            }
        }

        public async Task<bool> IndexDocumentAsync(Product product)
        {
            await CreateIndexAsync();
            var indexed = await _elasticClient.IndexDocumentAsync(product);
            return indexed.IsValid;
        }

        public async Task<ISearchResponse<Product>> SearchAsync(string query)
        {
            var response = await _elasticClient.SearchAsync<Product>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name)
                        .Query(query)
                    )
                )
            );

            return response;
        }
    }
}
