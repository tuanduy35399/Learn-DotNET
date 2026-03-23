using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TH_BT2.Models;

namespace TH_BT2.Controllers
{
    public class HomeController : Controller
    {
        DBIO db = new DBIO();

        public ActionResult Index()
        {
            List<NHANVIEN> list = db.getList_NHANVIEN();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NHANVIEN nv) {
            db.addObj(nv);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            NHANVIEN s = db.getObj_NHANVIEN(id);
            return View(s);
        }
        [HttpPost]
        public ActionResult Edit(NHANVIEN newNV)
        {
            db.editObj(newNV);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            db.getObj_NHANVIEN(id);
            return View();

        }
        [HttpPost]
        public ActionResult Delete(NHANVIEN nv)
        {
            db.delObj(nv);
            return RedirectToAction("Index");
        }
        public ActionResult Details (string id)
        {
            NHANVIEN nv= db.getObj_NHANVIEN(id);
            return View(nv);
        }
    }
}