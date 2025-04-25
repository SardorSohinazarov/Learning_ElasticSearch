using Nest;

namespace ElasticSearchCRUD.Services
{
    public partial class ElasticSearchService
    {
        private readonly IElasticClient _productsElasticClient;
        private readonly ILogger<ElasticSearchService> _logger;

        public ElasticSearchService(
            IConfiguration configuration,
            ILogger<ElasticSearchService> logger)
        {
            var _productsElasticClient = CreateElasticClientAsync(configuration);
            _logger = logger;
        }

        private IElasticClient CreateElasticClientAsync(IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration["ElasticSearch:Url"]))
                .DefaultIndex(configuration["ElasticSearch:Index"]);
            //.EnableApiVersioningHeader();                                                 // Headerlarni tekshirish kerak bo'lsa
            //.BasicAuthentication(configuration["username"], configuration["password"]);   //Agar login parol bilan o'rnatilsa

            return new ElasticClient(settings);
        }
    }
}
