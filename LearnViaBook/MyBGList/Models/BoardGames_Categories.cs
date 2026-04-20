using System.ComponentModel.DataAnnotations;

namespace MyBGList.Models
{
    public class BoardGames_Categories
    {
        [Key]
        [Required]
        public int BoardGameId { get; set; }
        [Key]
        [Required]
        public int CategoryId { get; set; }
        //BoardGames_Categories n-1 BoardGames
        public BoardGame? BoardGame { get; set; }
        //BoardGames_Categories n-1 Categories
        public Category? Category { get; set; }

    }
}
