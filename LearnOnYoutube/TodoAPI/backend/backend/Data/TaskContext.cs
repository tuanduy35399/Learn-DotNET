using Microsoft.EntityFrameworkCore;
using System;

namespace backend.Data
{
    public class TaskContext : DbContext //kế thừa lớp cha DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options): base(options) { }
        //bên Program sẽ truyền options vào TaskContext()
        // : base(options) -> gọi constructor cha để truyền options vô
        //Vì sao phải truyền lại cho lớp cha?
        //Bản chất vì kế thừa không tự động truyền tham số constructor.
        //Constructor của cha phải được gọi rõ ràng.
        /*
         * Khi bạn viết class TaskContext : DbContext, bạn kế thừa các phương thức (như SaveChanges,
         * Add, Remove). Nhưng các dữ liệu cấu hình (như chuỗi kết nối Database là gì, 
         * dùng SQL Server hay MySQL) thì lại nằm ở các biến private tận sâu bên trong lớp cha DbContext.
         * TaskContext không có quyền truy cập trực tiếp để tự cài đặt các biến private đó của lớp cha. 
         * Cách duy nhất để "bơm" cấu hình vào cho lớp cha hoạt động là thông qua Constructor của nó.
         * 
         * Quy tắc "Cha phải xây xong thì con mới có chỗ ở" 
         * Trong C#, khi một thực thể của lớp con được tạo ra, lớp cha luôn được khởi tạo trước.
         * Nếu bạn không gọi base(options), C# sẽ cố gắng tìm một constructor không tham số 
         * của DbContext để chạy.Tuy nhiên, DbContext cần các cấu hình (options) để biết đường mà kết nối DB. 
         * Nếu không truyền vào, "ông bố" sẽ không biết phải làm việc thế nào, và cả cái "nhà" 
         * TaskContext của bạn sẽ sụp đổ ngay lập tức.

         */
        public DbSet<backend.Models.TaskModel> Tasks { get; set; }
        /*
         DbSet<Task>: Đây là một kiểu dữ liệu đặc biệt của Entity Framework (EF). 
        Nó nói với EF rằng: "Tôi muốn tạo một tập hợp các đối tượng kiểu Task". 
        EF sẽ hiểu đây là một Bảng (Table).
         Tasks: Đây là tên thuộc tính bạn đặt. Trong code, bạn sẽ dùng tên này để truy vấn (ví dụ: _context.Tasks.ToList()). 
        Thông thường, tên bảng trong DB sẽ được EF tự động lấy theo tên này (thường để ở số nhiều).
         { get; set; }: Giúp EF Core có thể khởi tạo và quản lý danh sách này khi ứng dụng chạy.
         */
    }
}
