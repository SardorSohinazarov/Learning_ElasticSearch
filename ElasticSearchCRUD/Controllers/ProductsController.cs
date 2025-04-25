using ElasticSearchCRUD.Models;
using ElasticSearchCRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ElasticSearchService _elasticSearchService;

        public ProductsController(ElasticSearchService elasticSearchService)
            => _elasticSearchService = elasticSearchService;

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var indexed = await _elasticSearchService.IndexDocumentAsync(product);
            return Ok(indexed);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var response = await _elasticSearchService.SearchAsync(query);
            if (response == null || response.Count == 0)
                return NotFound("No results found");

            return Ok(response);
        }
    }
}
