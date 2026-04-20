using Microsoft.EntityFrameworkCore;

namespace MyBGList.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt): base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Khai báo composize key
            modelBuilder.Entity<BoardGames_Domains>()
            .HasKey(i => new { i.BoardGameId, i.DomainId });
            modelBuilder.Entity<BoardGames_Mechanics>()
            .HasKey(i => new { i.BoardGameId, i.MechanicId });
            //Tạo quan hệ 1-n giữa BoardGames_Domains và BoardGame
            modelBuilder.Entity<BoardGames_Domains>()
                .HasOne(x => x.BoardGame)
                .WithMany(y => y.BoardGames_Domains)
                .HasForeignKey(f => f.BoardGameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            //Tạo quan hệ 1-n giữa BoardGames_Domains và Domain
            modelBuilder.Entity<BoardGames_Domains>()
                .HasOne(x => x.Domain)
                .WithMany(y => y.BoardGames_Domains)
                .HasForeignKey(f => f.DomainId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            //Tạo quan hệ 1-n giữa BoardGames_Mechanics và BoardGame
            modelBuilder.Entity<BoardGames_Mechanics>()
                .HasOne(x => x.BoardGame)
                .WithMany(y => y.BoardGames_Mechanics)
                .HasForeignKey(f => f.BoardGameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            //Tao qh 1-n giua BoardGames_Mechanics va Mechanic
            modelBuilder.Entity<BoardGames_Mechanics>()
                .HasOne(x => x.Mechanic)
                .WithMany(y => y.BoardGames_Mechanics)
                .HasForeignKey(f => f.MechanicId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
        //Tạo các DbSet
        //DbSet đại diện cho 1 table còn các class mà ta đã tạo trước đó đại diện cho 1 dòng record
        //Đó là lý do vì sao tên class không có "s" còn tên table được khai báo tường minh lại có "s"
        public DbSet<BoardGame> BoardGames => Set<BoardGame>(); //trả về tập các dòng dữ liệu có kiểu BoardGame
        public DbSet<Domain> Domains => Set<Domain>();
        public DbSet<Mechanic> Mechanics => Set<Mechanic>();
        public DbSet<BoardGames_Mechanics> BoardGames_Mechanics => Set<BoardGames_Mechanics>();
        public DbSet<BoardGames_Domains> BoardGames_Domains => Set<BoardGames_Domains>();

    }
}
