using Practice2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practice2.Controllers
{
    public class HomeController : Controller
    {
        DBIO db = new DBIO();
        public ActionResult Index()
        {
            //ViewBag.Whoareyou = "Bạn là ai";
            //ViewBag.AnswerText = "Nhập tên bạn vào đây";
            ViewBag.LabelTimKiem = "Tìm kiếm ";
            List<SINHVIEN> ds = db.getList_HV();
            ViewData["TimKiem"] = new SelectList(ds, "MAHOCVIEN", "TENHOCVIEN");
            List<SINHVIEN> listSV = db.getList_HV();
            return View(listSV);
        }

        public ActionResult TimKiem(string id)
        {
            ViewBag.LabelTimKiem = "Tìm kiếm ";
            List<SINHVIEN> ds = db.getList_HV();
            ViewData["TimKiem"] = new SelectList(ds, "MAHOCVIEN", "TENHOCVIEN");
            SINHVIEN sv = db.getDetail(id);
            List<SINHVIEN> kq= new List<SINHVIEN>();
            if(sv!=null)
            {
                kq.Add(sv);
            }

            return View("Index", kq);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SINHVIEN sv)
        {
            db.createSV(sv);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            SINHVIEN sv = db.getDetail(id);
            return View( sv);
        }
        [HttpPost]
        public ActionResult Edit(SINHVIEN newSV)
        {
            db.upadateSV(newSV);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)
        {
            SINHVIEN sv = db.getDetail(id);
            return View(sv);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(string id)
        {
            db.deleteSV(id);
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            SINHVIEN sv= db.getDetail(id);
            return View(sv);
        }
    }
}