using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice2.Models
{
    public class DBIO
    {
        QLHOCVIENEntities1 mdb = new QLHOCVIENEntities1();

        public List<SINHVIEN> getList_HV()
        {
            return mdb.SINHVIENs.ToList();
        }
        public SINHVIEN getDetail(string id)
        {
            return mdb.SINHVIENs.Find(id);

        }
        public void createSV(SINHVIEN sv)
        {
            mdb.SINHVIENs.Add(sv);
            mdb.SaveChanges();
        }
        public void upadateSV(SINHVIEN newSV)
        {
            SINHVIEN sv = getDetail(newSV.MAHOCVIEN);
            if(sv!=null)
            {
                mdb.Entry(sv).CurrentValues.SetValues(newSV);
            }
            mdb.SaveChanges();

        }
        public void deleteSV(string id)
        {
            SINHVIEN sv = mdb.SINHVIENs.Find(id);
            mdb.SINHVIENs.Remove(sv);
            mdb.SaveChanges();
        }
    }
}