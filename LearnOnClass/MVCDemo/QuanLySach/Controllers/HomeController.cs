using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySach.Controllers
{
    public class HomeController : Controller
    {
        DBIO db = new DBIO();

        // GET: Home
        public ActionResult Index()
        {
            List<SACH> list = db.getList_SACH();
            return View(list);
        }

        //Không dùng tham số gì cả cho Create()
        public ActionResult Create()
        {
            return View();
        }
        //Khi gọi đến phương thức addObject_SACH(SACH s) in DBIO.cs
        //thì đồng thời ta cần một thông điệp nào đó để nó load dữ liệu lên
        //=> dùng [HttpPost]
        [HttpPost]
        public ActionResult Create(SACH s)
        {
            db.addObject_SACH(s);
            //sau khi thêm rồi, ta điều hướng người dùng đến trang Index
            return RedirectToAction("Index");
        }
        //Sửa sách phức tạp hơn thêm (create) vì ta cần biết sửa sách nào?
        //Ta cần biết id của quyển sách bạn đang định sửa
        public ActionResult Edit(string id)
        {
            SACH s = db.getObject_SACH(id);
            return View(s); //lấy ra quyển sách thì sau đó mới sửa được
        }
        [HttpPost] //Ý nghĩa là khi bạn gọi Edit nó cần gửi lên thông điệp để sửa thông tin này
        public ActionResult Edit(SACH s) //phải đặt tên là Edit như hàm bên trên, không dùng tên khác
        {
            db.editObject_SACH(s);
            return RedirectToAction("Index"); //Khi sửa rồi thì hệ thống sẽ điều hướng quay về trang chủ
        }
        //Xóa một quyển sách
        //Ta cần biết id của quyển sách bạn đang định xóa
        public ActionResult Delete(string id)
        {
            SACH s = db.getObject_SACH(id);
            return View(s); //lấy ra quyển sách thì sau đó mới xóa được
        }
        [HttpPost] //Ý nghĩa là khi bạn gọi Delete nó cần gửi lên thông điệp để xóa thông tin này
        public ActionResult Delete(SACH s)
        {
            db.deleteObject_SACH(s);
            return RedirectToAction("Index"); //Khi xóa rồi thì hệ thống sẽ điều hướng quay về trang chủ
        }
        //Xem một quyển sách
        //Ta cần biết id của quyển sách bạn đang định xem chi tiết
        public ActionResult Details(string id)
        {
            SACH s = db.getObject_SACH(id);
            return View(s); //lấy ra quyển sách cần xem
        }

        
    }
}