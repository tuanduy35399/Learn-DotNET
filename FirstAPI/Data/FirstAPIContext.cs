using Microsoft.EntityFrameworkCore;
using FirstAPI.Models; 

namespace FirstAPI.Data
{
    public class FirstAPIContext : DbContext
    {
        public FirstAPIContext(DbContextOptions<FirstAPIContext> options)
            : base(options) /*gọi hàm xây dựng lớp DbContext "Lấy những cấu hình từ tham số options này 
                             * và chuyển ngược lên cho lớp cha DbContext xử lý"
                             * */
        {
        }

        // Khai báo các bảng dữ liệu
        public DbSet<Book> Books { get; set; }
    }
}