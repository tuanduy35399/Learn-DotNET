using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBGList.DTO;
using System.Xml.Linq;
using MyBGList.Models;
using Microsoft.EntityFrameworkCore;
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
        //Đây là REST API Chuẩn HATEOAS
        [HttpGet(Name = "GetBoardGames")]
        public async Task<RestDTO<BoardGame[]>> Get() 
            {
            var query = _context.BoardGames;
            return new RestDTO<BoardGame[]>() 
            {
            //Data = new BoardGame[] { //Thay vì tạo 1 instance mới như vầy thì ta dùng Dependency Injection
            Data= await query.ToArrayAsync(), 
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
