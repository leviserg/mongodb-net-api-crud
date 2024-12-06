using Microsoft.AspNetCore.Mvc;
using mongodb_net_api_crud.Models;
using mongodb_net_api_crud.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mongodb_net_api_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // POST: api/<BooksController>
        [HttpPost]
        [Route("fetch")]
        public async Task<ActionResult<BooksDto>> GetBooks([FromBody] Filter filter)
        {
            try
            {
                var books = await _bookService.GetBooksAsync(filter);
                return Ok(books);
            } catch (Exception ex) { 
                return NotFound(ex.Message);
            }

        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            try
            {
                var book = await _bookService.GetBookAsync(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<Book>> CreateOrUpdate([FromBody] Book book)
        {
            try
            {
                var savedBook = await _bookService.CreateOrUpdateBookAsync(book);
                return Ok(savedBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
