using PracticeMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticeMVC.Controllers
{
    public class MainController : Controller
    {
        DBIO db = new DBIO();
        public ActionResult Index()
        {
            List <SINHVIEN> list = db.getList_SinhVien();
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SINHVIEN sv)
        {
            db.add_SinhVien(sv);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            SINHVIEN sv = db.getDetail_SinhVien(id);
            return View(sv);

        }

        [HttpPost] 
        public ActionResult Edit(SINHVIEN sv)
        {
            db.update_SinhVien(sv);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            SINHVIEN sv = db.getDetail_SinhVien(id);
            return View(sv);
        }
        [HttpPost]
        public ActionResult Delete(SINHVIEN sv)
        {
            db.delete_SinhVien(sv);
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            SINHVIEN s= db.getDetail_SinhVien(id);
            return View(s);
        }
            
    }
}