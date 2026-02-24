using FirstAPI.Data;
using FirstAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly FirstAPIContext _context;
        public BooksController(FirstAPIContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook == null) return BadRequest();
             _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
            //Hàm này nó sẽ trả về kết quả HTTP 201 Created.
            /*
             return CreatedAtAction(
                nameof(GetBookById),           // 1. Tên của hàm (Action) dùng để lấy chi tiết 
                new { id = newBook.Id },       // 2. Tham số truyền vào hàm đó (để tạo ra URL)
                newBook                        // 3. Dữ liệu thực tế trả về cho người dùng
            );
            nameof(GetBookById): Đây là tên hàm dùng để "xem chi tiết". Khi bạn dùng nameof, 
            nếu sau này bạn đổi tên hàm, Visual Studio sẽ tự động cập nhật giúp bạn, tránh bị lỗi gõ sai chữ.

            new { id = newBook.Id }: Đây là giá trị truyền vào hàm xem chi tiết.
            Bạn lưu ý là ở bước này, newBook.Id đã có giá trị (do SQL Server vừa gán sau lệnh SaveChangesAsync).

            newBook: Trả lại toàn bộ nội dung cuốn sách dưới dạng JSON trong phần thân (Body) của kết quả. 
            Điều này giúp Client biết được các thông tin khác (như ID) mà không cần phải gọi thêm một lệnh GET nữa.
             */
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            //Dùng IActionResult là vì nó chỉ trả về status code, không trả về data
            //Tim book co id do
            var book = await _context.Books.FindAsync(id);
            if (book == null) return BadRequest();
            //Tien hanh cap nhat book[id]
            //book.Id = updatedBook.Id; do Id là key không đổi được nên bỏ dòng này
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            await _context.SaveChangesAsync();
            return NoContent(); //khong tra ve data, chi tra status code 204
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return BadRequest();
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
