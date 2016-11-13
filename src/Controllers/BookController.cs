using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WayOfWork.Domain;
using WayOfWork.Services;

namespace WayOfWork.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        public BookController()
        {
            DbContext = new ContrivedDatabaseContext();
        }

        public ContrivedDatabaseContext DbContext { get; private set; }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await DbContext.LoadAll();
            return Ok(result);
        }

        [HttpGet("{id}", Name="DefaultGet")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                return BadRequest("Id must be greater than zero when trying to access a book");
            var result = await DbContext.Get(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BookRequest bookRequest)
        {
            var validationResult = ValidateBook(bookRequest);
            if (validationResult.Any())
                return BadRequest(validationResult);
            var book = MapRequestToBook(bookRequest);
            book.Id = 0;
            var addResult = await DbContext.Insert(book);
            if (addResult == 0)
                return StatusCode(StatusCodes.Status409Conflict);
            var result = new BookAddResult {Id = addResult};
            return CreatedAtRoute("DefaultGet", new BookAddResult { Id = addResult }, result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]BookRequest bookRequest)
        {
            var validationResult = ValidateBook(bookRequest);
            if (id < 1)
                validationResult.Add("Not Id specified for book");
            if (validationResult.Any())
                return BadRequest(validationResult);
            var book = MapRequestToBook(bookRequest);
            book.Id = id;
            var result = await DbContext.Update(book);
            if (result == 0)
                return NotFound();
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest("Id must be greater than zero when trying to delete a book");
            var deleteResult = await DbContext.Delete(id);
            if (deleteResult == 0)
                return NotFound();
            return Ok();
        }

        private IList<string> ValidateBook(BookRequest book)
        {
            var result = new List<string>();
            if (string.IsNullOrWhiteSpace(book.Name))
                result.Add("Book must have a description");
            if (string.IsNullOrWhiteSpace(book.Author))
                result.Add("Book must have a author");
            if (book.Price <= 0)
                result.Add("Book must have a price");
            return result;
        }

        private Book MapRequestToBook(BookRequest bookRequest)
        {
            return new Book
            {
                Author = bookRequest.Author,
                Name = bookRequest.Name,
                Price = bookRequest.Price
            };
        }
    }
}
