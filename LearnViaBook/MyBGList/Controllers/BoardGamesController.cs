using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBGList.DTO;
using System.Xml.Linq;
using MyBGList.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
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
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration =60)]
        public async Task<RestDTO<BoardGame[]>> Get(
            int pageIndex=0, 
            int pageSize=10, 
            string? sortColumn = "Name",
            string? sortOrder = "ASC",
            string? filterQuery=null
            )
            {

            var query = _context.BoardGames.AsQueryable(); //AsQueryable giúp biến query thành IQueryable
            //có thể tiếp tục build và xử lý bên DB thay vì RAM
            if (!string.IsNullOrEmpty(filterQuery))
                query.Where(b => b.Name.Contains(filterQuery));
            var recordCount = await query.CountAsync();
            query= query
                .OrderBy($"{sortColumn} {sortOrder}") //dùng dynamic LINQ thay cho lamda expression
                //phải có dấu cách ở giữa
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
            return new RestDTO<BoardGame[]>() 
            {
            //Data = new BoardGame[] { //Thay vì tạo 1 instance mới như vầy thì ta dùng Dependency Injection
            Data= await query.ToArrayAsync(),
            PageIndex = pageIndex,
            PageSize = pageSize, 
            RecordCount= recordCount,
            Links = new List<LinkDTO> { 
            new LinkDTO(
            Url.Action(null, "BoardGames", new {pageIndex,pageSize}, Request.Scheme)!,
            "self",
            "GET"),
            }
            };
            }
    }
}
