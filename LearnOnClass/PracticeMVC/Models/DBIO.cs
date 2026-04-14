using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticeMVC.Models
{
    public class DBIO
    {
        QLHOCVIENEntities1 myDataBase = new QLHOCVIENEntities1();
        public List<SINHVIEN> getList_SinhVien()
        {
            return myDataBase.SINHVIENs.ToList();
        }
        public SINHVIEN getDetail_SinhVien(string id)
        {
            return myDataBase.SINHVIENs.Where(element => element.MAHOCVIEN.Equals(id)).FirstOrDefault();
        }
        public void add_SinhVien(SINHVIEN sinhvien)
        {
            myDataBase.SINHVIENs.Add(sinhvien);
            myDataBase.SaveChanges();
        }
        public void update_SinhVien(SINHVIEN newSinhVien)
        {
            SINHVIEN oldSinhVien = getDetail_SinhVien(newSinhVien.MAHOCVIEN);
            oldSinhVien.MAHOCVIEN = newSinhVien.MAHOCVIEN;
            oldSinhVien.TENHOCVIEN = newSinhVien.TENHOCVIEN;
            oldSinhVien.NGAYSINH = newSinhVien.NGAYSINH;
            oldSinhVien.DIENTHOAI = newSinhVien.DIENTHOAI;
            oldSinhVien.EMAIL = newSinhVien.EMAIL;
            oldSinhVien.PROFILE = newSinhVien.PROFILE;
            oldSinhVien.GIOITINH = newSinhVien.GIOITINH;
            myDataBase.SaveChanges();

        }
        public void delete_SinhVien(SINHVIEN sinhVien)
        {
            SINHVIEN oldSinhVien = getDetail_SinhVien(sinhVien.MAHOCVIEN);
            myDataBase.SINHVIENs.Remove(oldSinhVien);
            myDataBase.SaveChanges();
        }
    }
}