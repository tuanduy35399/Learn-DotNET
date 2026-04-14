using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class TaskModel
    {
        [Key] //ép EF nhận Id làm khóa chính
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Body { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}
