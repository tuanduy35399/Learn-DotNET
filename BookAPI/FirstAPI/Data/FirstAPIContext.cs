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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
                 new Book
                 {
                     Id = 1,
                     Title = "Hoo",
                     Author = "Tuan Duy",
                     YearPublished = 2026,
                 },
            new Book
            {
                Id = 2,
                Title = "Hoo2",
                Author = "Tuan Duy2",
                YearPublished = 2027,
            }
            );
        }

        // Khai báo các bảng dữ liệu
        public DbSet<Book> Books { get; set; }
        
    }
}