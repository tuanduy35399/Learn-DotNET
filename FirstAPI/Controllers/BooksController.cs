using FirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id=1,
                Title= "Hoo",
                Author= "Tuan Duy",
                YearPublished= 2026,
            },
            new Book
            {
                Id=2,
                Title= "Hoo2",
                Author= "Tuan Duy2",
                YearPublished= 2027,
            }
        };
        [HttpGet]
        public ActionResult<List<Book>> GetBook()
        {
            return Ok(books);
        }
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpPost]
        public ActionResult<Book> AddBook(Book newBook)
        {
            if (newBook == null) return BadRequest();
            books.Add(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
            //Hàm này nó sẽ trả về kết quả HTTP 201 Created.
            /*
             return CreatedAtAction(
                nameof(GetBookById),           // 1. Tên của hàm (Action) dùng để lấy chi tiết 
                new { id = newBook.Id },       // 2. Tham số truyền vào hàm đó (để tạo ra URL)
                newBook                        // 3. Dữ liệu thực tế trả về cho người dùng
            );
             */
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            //Dùng IActionResult là vì nó chỉ trả về status code, không trả về data
            //Tim book co id do
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null) return BadRequest();
            //Tien hanh cap nhat book[id]
            book.Id = updatedBook.Id;
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            return NoContent(); //khong tra ve data, chi tra status code 204
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null) return BadRequest();
            books.Remove(book);
            return NoContent();
        }
    }
}
