using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySach.Models
{
    public class DBIO
    {
        QLSACHEntities1 mydb = new QLSACHEntities1();
        //hàm getList_SACH() để thực hiện một câu truy vấn
        //trả về một danh sách dùng iQuery
        public List<SACH> getList_SACH()
        {
            //access to the SACH table to read all rows
            return mydb.SACHes.ToList();
        }
        //hàm getObject_SACH(string id) trả về một quyển sách theo id
        public SACH getObject_SACH(string id)
        {
            //biểu thức so sánh có cú pháp như C#
            //chứ không phải của SQL
            return mydb.SACHes.Where(c => c.MASACH == id).FirstOrDefault();
        }
        //hàm addObject_SACH(SACH s) thêm vào một quyển sách
        public void addObject_SACH(SACH s)
        {
            mydb.SACHes.Add(s);
            mydb.SaveChanges();
        }
        //hàm editObject_SACH(SACH s) sửa một quyển sách
        public void editObject_SACH(SACH s)
        {
            SACH x = getObject_SACH(s.MASACH);
            x.TENSACH = s.TENSACH;
            x.TACGIA = s.TACGIA;
            x.NAMXB = s.NAMXB;
            x.SOLUONG = s.SOLUONG;
            x.IMAGECOVER = s.IMAGECOVER;
            mydb.SaveChanges();
        }
        //hàm deleteObject_SACH(SACH s) xóa một quyển sách s
        public void deleteObject_SACH(SACH s)
        {
            SACH x = getObject_SACH(s.MASACH);
            mydb.SACHes.Remove(x);
            mydb.SaveChanges();
        }

    }
}

