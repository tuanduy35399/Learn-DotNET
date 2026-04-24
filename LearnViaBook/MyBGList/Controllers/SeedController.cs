using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList.Models;
using System.Globalization;

namespace MyBGList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SeedController> _logger;
        //add log env
        private readonly IWebHostEnvironment _env;

        public SeedController(ApplicationDbContext context, ILogger<SeedController> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }
        [HttpPut(Name = "Seed")]
        [ResponseCache(NoStore =true)]
        public async Task<IActionResult> Put()
        {
            var config = new CsvConfiguration(CultureInfo.GetCultureInfo("pt-BR")) //pt-BR là mã chuẩn quốc tế
            //pt- Portuguese ; BR- Brazil => 1,5 
            {
                HasHeaderRecord = true, //csv có dòng đầu là header
                Delimiter = ";",//ngăn cách bởi ";"
            };
            //dùng using khi dùng xong tự dọn dẹp (dispose) tài nguyên
            using var reader = new StreamReader( //mở file csv để đọc
                System.IO.Path.Combine(_env.ContentRootPath, "Data/bgg_dataset.csv"));
            using var csv = new CsvReader(reader, config); //đọc từng dòng csv rồi convert sang C#
            //load dữ liệu từ DB mục đích để chống duplicate
            //Dùng dictionary để tối ưu hiệu năng 
            //không dùng any vì mỗi lần cần là nó lại gọi DB => gọi quá nhiều truy vấn gây lag
            //dùng dict thì gọi DB 1 lần load tất cả vô RAM và truy vấn trên RAM
            var existingBoardGames = await _context.BoardGames.ToDictionaryAsync(bg => bg.Id);
            var existingDomains = await _context.Domains.ToDictionaryAsync(dm => dm.Name);
            var existingMechanics = await _context.Mechanics.ToDictionaryAsync(m => m.Name);
            var now = DateTime.Now;
        }



    }
}
