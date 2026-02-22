using Microsoft.EntityFrameworkCore;
using FirstAPI.Models; // Đảm bảo có dòng này để dùng được class User, Book

namespace FirstAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Khai báo các bảng dữ liệu của bạn ở đây
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}