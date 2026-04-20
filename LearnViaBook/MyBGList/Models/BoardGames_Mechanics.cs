using System.ComponentModel.DataAnnotations;

namespace MyBGList.Models
{
    public class BoardGames_Mechanics
    {
        [Required]
        [Key]
        public int BoardGameId { get; set; }
        [Required]
        [Key]
        public int MechanicId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        //trỏ tới 2 bảng nhằm để join trong code
        public BoardGame? BoardGame { get; set; } 
        public Mechanic? Mechanic { get; set; }

    }
}
