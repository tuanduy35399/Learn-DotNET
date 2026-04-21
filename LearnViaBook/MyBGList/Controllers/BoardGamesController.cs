using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBGList.DTO;
using System.Xml.Linq;
using MyBGList.Models;
namespace MyBGList.Controllers
{
    [Route("[controller]")] //endpoint là tên của Controller bỏ đi cụm Controller => /BoardGames
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        //Tạo instance của ApplicationDbContext để giao tiếp với database
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(ApplicationDbContext context, ILogger<BoardGamesController> logger)
        {
            _logger = logger;
            _context = context;
        }

        //[HttpGet(Name = "GetBoardGames")] //đặt định danh route để sinh route tự động,
        //khi route chính có thay đổi thì ko ảnh hưởng gì đến các route con
        //là 1 cách để gọi thay vì viết đầy đủ ra /BoardGames/score ,...thì chỉ cần gọi 
        //GetBoardGames
        //public IEnumerable<BoardGame> Get()
        //{
        //    return new[] { //tạo mảng nặc danh 
        //            new BoardGame() {
        //            Id = 1,
        //            Name = "Axis & Allies",
        //            Year = 1981,
        //            MaxPlayers=90,
        //            MinPlayers=2,
        //            },
        //            new BoardGame() {
        //            Id = 2,
        //            Name = "Citadels",
        //            Year = 2000,
        //            MaxPlayers=90,
        //            MinPlayers=2,
        //            },
        //            new BoardGame() {
        //            Id = 3,
        //            Name = "Terraforming Mars",
        //            Year = 2016,
        //            MaxPlayers=90,
        //            MinPlayers=2,
        //            }
        //     };
        //}
        //[HttpGet("{id}", Name = "GetDetail")]
        //public BoardGame GetDetail()
        //{
        //    return 
        //}

        //Đây là REST API Chuẩn HATEOAS
        [HttpGet(Name = "GetBoardGames")]
        public RestDTO<BoardGame[]> Get() 
            {
            return new RestDTO<BoardGame[]>() 
            {
            Data = new BoardGame[] {
            new BoardGame()
                    {
                        Id = 1,
            Name = "Axis & Allies",
            Year = 1981
            },
            new BoardGame()
                    {
                        Id = 2,
            Name = "Citadels",
            Year = 2000
            },
            new BoardGame()
                    {
                        Id = 3,
            Name = "Terraforming Mars",
            Year = 2016
            }
                },
            Links = new List<LinkDTO> { 
            new LinkDTO(
            Url.Action(null, "BoardGames", null, Request.Scheme)!,
            "self",
            "GET"),
            }
            };
            }
    }
}
