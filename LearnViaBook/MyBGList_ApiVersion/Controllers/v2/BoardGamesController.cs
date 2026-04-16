using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBGList_ApiVersion.DTO.v2;
using System.Xml.Linq;

namespace MyBGList_ApiVersion.Controllers.v2
{
    [Route("/v{version:ApiVersion}/[controller]")] //endpoint là tên của Controller bỏ đi cụm Controller => /BoardGames
    [ApiController]
    [ApiVersion("2.0")]
    [ResponseCache(Duration= 120, Location=ResponseCacheLocation.Client)]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;
        public BoardGamesController(ILogger<BoardGamesController> logger)
        {
            _logger = logger;
        }

        //Đây là REST API Chuẩn HATEOAS
        [HttpGet(Name = "GetBoardGames")]
        public RestDTO<BoardGame[]> Get() 
            {
            return new RestDTO<BoardGame[]>() 
            {
            Item = new BoardGame[] {
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
            Links = new List<DTO.v1.LinkDTO> { 
            new DTO.v1.LinkDTO(
            Url.Action(null, "BoardGames", null, Request.Scheme)!,
            "self",
            "GET"),
            }
            };
            }
    }
}
