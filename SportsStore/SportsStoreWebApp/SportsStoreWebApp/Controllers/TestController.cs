using Microsoft.AspNetCore.Mvc;

namespace SportsStoreWebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Chào mừng bạn đến với Cửa hàng Thể thao! Đây là trang Test!";
            return View();
        }

        // Một Action Method khác: /Test/HelloWorld
        public IActionResult HelloWorld()
        {
            return Content("Xin chào từ Action HelloWorld của TestController!");
        }
        // Action nhận tham số: /Test/Welcome?name=DavidTeo
        public IActionResult Welcome(string name = "Khách")
        {
            return Content($"Chào mừng {name} đến với trang Test!");
        }
    }
}
