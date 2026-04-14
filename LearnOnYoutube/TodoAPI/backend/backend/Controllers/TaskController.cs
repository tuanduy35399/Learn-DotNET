using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*
     [Route("api/[controller]")]: Xác định địa chỉ URL để gọi vào API này.

Cụm [controller] là một "token". Nó sẽ tự lấy tên của Class (bỏ chữ Controller đi) để làm đường dẫn.

Ví dụ: Class là TaskController thì đường dẫn sẽ là .../api/task. 
    
Nếu bạn đổi tên class thành UserController, đường dẫn tự động là .../api/user.

[ApiController]: Đây là một "chứng chỉ" xác nhận class này phục vụ cho Web API. 

Nó giúp tự động thực hiện các việc như:

Kiểm tra dữ liệu đầu vào (Validation) có hợp lệ không.

Trả về lỗi 400 (Bad Request) nếu dữ liệu gửi lên bị sai cấu trúc mà bạn không cần viết 

code kiểm tra thủ công.
     */
    public class TaskController : ControllerBase
    /*Tại sao không phải là Controller?
     * ControllerBase: Chỉ chứa các tính năng cần thiết cho API (trả về dữ liệu JSON, trạng thái HTTP 200, 404...).
     * Controller: Thường dùng cho các website cũ (MVC) có giao diện HTML. 
     * Vì chúng ta đang làm API, dùng ControllerBase sẽ nhẹ và tối ưu hơn.
     */
    {
        private readonly TaskContext _context; //Tạo đối tượng A
        /*Tại sao không dùng _context = new TaskContext(...)? 
         * Nếu bạn tự new, bạn phải tự lo chuỗi kết nối, cấu hình database... cực kỳ rắc rối.
         */
        public TaskController(TaskContext context) //Hàm xây dựng 
        {
            _context = context; //lưu tham số nhận được vào đối tượng A
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> GetListTask()
        {
            return Ok(await _context.Tasks.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetDetailTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }
        [HttpPost]
        public async Task<ActionResult<TaskModel>> AddTask(TaskModel newTask)
        {
            if (newTask == null) return BadRequest();
            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDetailTask), new { id = newTask.Id }, newTask);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskModel updatedTask)
        {
            if (updatedTask == null) return BadRequest();
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            task.Title = updatedTask.Title;
            task.Body = updatedTask.Body;
            task.Author = updatedTask.Author;
            task.UpdatedAt = DateTime.Now;
            task.IsCompleted = updatedTask.IsCompleted;
            await _context.SaveChangesAsync();
            //return NoContent(); //204 no data
            return Ok(task); //200 return data
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
