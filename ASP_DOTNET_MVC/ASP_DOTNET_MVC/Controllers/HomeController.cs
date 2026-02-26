using System.Diagnostics;
using ASP_DOTNET_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_DOTNET_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository; //readonly đảm bảo chỉ gán 1 lần duy nhất
        private readonly ILogger<HomeController> _logger;

        public HomeController(IRepository repository, ILogger<HomeController> logger)
        {
            /*public HomeController(IRepository repository, ILogger<HomeController> logger): 
             * Đây là dòng quan trọng nhất. Thay vì tự tạo mới (new MyRepository()), 
             * Controller này "yêu cầu" hệ thống cấp cho nó một đối tượng nào đó thực thi IRepository.
             */
            this.repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new HelloModel() { Name = "Tuan Duy", Age = 20 });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NewActionMethod()
        {
            return Content("Hi from NewActionMethod " + repository.GetById("My ID"));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
