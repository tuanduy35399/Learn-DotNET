using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBGList.Controllers
{
    [Route("[controller]")] //endpoint là tên của Controller bỏ đi cụm Controller => /BoardGames
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;
        public BoardGamesController(ILogger<BoardGamesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBoardGames")] //đặt định danh route để sinh route tự động,
                                          //khi route chính có thay đổi thì ko ảnh hưởng gì đến các route con
                                          //là 1 cách để gọi thay vì viết đầy đủ ra /BoardGames/score ,...thì chỉ cần gọi 
                                          //GetBoardGames
        public IEnumerable<BoardGame> Get()
        {
            return new[] { //tạo mảng nặc danh 
                    new BoardGame() {
                    Id = 1,
                    Name = "Axis & Allies",
                    Year = 1981,
                    MaxPlayers=90,
                    MinPlayers=2,
                    },
                    new BoardGame() {
                    Id = 2,
                    Name = "Citadels",
                    Year = 2000,
                    MaxPlayers=90,
                    MinPlayers=2,
                    },
                    new BoardGame() {
                    Id = 3,
                    Name = "Terraforming Mars",
                    Year = 2016,
                    MaxPlayers=90,
                    MinPlayers=2,
                    }
             };
        }
        //[HttpGet("{id}", Name = "GetDetail")]
        //public BoardGame GetDetail()
        //{
        //    return 
        //}
    }
}
