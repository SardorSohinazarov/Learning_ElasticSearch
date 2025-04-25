using ElasticSearchCRUD.Models;
using Nest;
using Newtonsoft.Json;

namespace ElasticSearchCRUD.Services
{
    public partial class ElasticSearchService
    {
        /// <summary>
        /// Malumotlardan foydalanib index yaratish
        /// Agar malumotlar update qilish kerak bo'lsa id bilan qayta indexlansa malumotlar yangilanadi
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> IndexDocumentAsync(Product product)
        {
            var indexResponse = await _productsElasticClient.IndexDocumentAsync(product);

            if (!indexResponse.IsValid)
            {
                _logger.LogError($"Failed to index document: {indexResponse.OriginalException}");
                Console.WriteLine(JsonConvert.SerializeObject(indexResponse.OriginalException));
            }

            return indexResponse.IsValid;
        }

        /// <summary>
        /// Xira qidirish :)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Product>> SearchAsync(string query)
        {
            var response = await _productsElasticClient.SearchAsync<Product>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.NameUz)
                        .Field(f => f.NameEn)
                        .Field(f => f.NameRu)
                        .Query(query)
                        .Fuzziness(Fuzziness.Auto)
                    )
                )
            );

            return response.Documents.ToList();
        }

        /// <summary>
        /// Malumotlarni id bilan olish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetByIdAsync(string id)
        {
            var getResponse = await _productsElasticClient.GetAsync<Product>(id);
            if (getResponse.IsValid)
                return getResponse.Source;
            return null;
        }

        /// <summary>
        /// Barcha malumotlarni olish
        /// </summary>
        /// <returns></returns>
        public async Task<Product> GetAllAsync()
        {
            var esResponse = await _productsElasticClient.SearchAsync<Product>();

            return esResponse.Documents.FirstOrDefault();
        }

        /// <summary>
        /// Malumotlarni o'chirish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            var deleteResponse = await _productsElasticClient.DeleteAsync<Product>(id);
            if (!deleteResponse.IsValid)
            {
                _logger.LogError($"Failed to delete document: {deleteResponse.OriginalException}");
                Console.WriteLine(JsonConvert.SerializeObject(deleteResponse.OriginalException));
            }

            return deleteResponse.IsValid;
        }
    }
}
