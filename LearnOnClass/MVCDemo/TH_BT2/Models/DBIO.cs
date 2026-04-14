using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TH_BT2.Models
{
    public class DBIO
    {
        MaSV_tenSVEntities1 mdb = new MaSV_tenSVEntities1();

        public List<NHANVIEN> getList_NHANVIEN()
        {
            return mdb.NHANVIENs.ToList();
        }

        public NHANVIEN getObj_NHANVIEN(string id)
        {
            return mdb.NHANVIENs.Where(t => t.MANHANVIEN.Equals(id)).FirstOrDefault();
        }

        public void addObj(NHANVIEN nv)
        {
            mdb.NHANVIENs.Add(nv);
            mdb.SaveChanges();
        }

        public void editObj(NHANVIEN newnv)
        {
            NHANVIEN  nv = getObj_NHANVIEN(newnv.MANHANVIEN);
            nv.TENNHANVIEN = newnv.TENNHANVIEN;
            nv.NGAYSINH = newnv.NGAYSINH;
            nv.GIOITINH= newnv.GIOITINH;
            nv.EMAIL= newnv.EMAIL;
            nv.DIENTHOAI = newnv.DIENTHOAI;
            nv.PROFILE = newnv.PROFILE;
            mdb.SaveChanges();
        }
        public void delObj(NHANVIEN nv)
        {
            NHANVIEN x = getObj_NHANVIEN(nv.MANHANVIEN);
            mdb.NHANVIENs.Remove(x);    
            mdb.SaveChanges();
        }
     }
}