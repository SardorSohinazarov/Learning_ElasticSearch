using ElasticSearchCRUD.Models;
using ElasticSearchCRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookElasticSearchController : ControllerBase
    {
        private readonly IBookElasticSearchCRUDService _bookElasticSearchCRUDService;

        public BookElasticSearchController(IBookElasticSearchCRUDService bookElasticSearchCRUDService)
            => _bookElasticSearchCRUDService = bookElasticSearchCRUDService;

        [HttpPost]
        public async Task<IActionResult> CreateBookAsync(Book book)
        {
            var isCreated = await _bookElasticSearchCRUDService.CreateBookAsync(book);
            return Ok(isCreated);
        }

        [HttpGet("all-books")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _bookElasticSearchCRUDService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookAsync(int id)
        {
            var book = await _bookElasticSearchCRUDService.GetBookAsync(id);
            return Ok(book);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookAsync(Book book)
        {
            var isUpdated = await _bookElasticSearchCRUDService.UpdateBookAsync(book);
            return Ok(isUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var isDeleted = await _bookElasticSearchCRUDService.DeleteBookAsync(id);
            return Ok(isDeleted);
        }

        [HttpGet("search")]
        public async Task<IActionResult> FuzzySearchBooksAsync(string query)
        {
            var books = await _bookElasticSearchCRUDService.FuzzySearchBooksAsync(query);
            return Ok(books);
        }
    }
}
