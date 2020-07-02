using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.BLL;

    public class EmployeesSvc : GenericSvc<EmployeesRep, Employees>
    {
        public object GetDoanhThuNhanVienTrongNgay(DateTime date)
        {
            return _rep.GetDoanhThuNhanVienTrongNgay(date);
        }

        public object GetDoanhThuNhanVienTrongKhoangThoiGian(DateTime dateBegin, DateTime dateEnd)
        {
            return _rep.GetDoanhThuNhanVienTrongKhoangThoiGian(dateBegin, dateEnd);
        }

        public object GetDoanhThuNhanVienTrongNgay_LinQ(DateTime date)
        {
            return _rep.GetDoanhThuNhanVienTrongNgay_LinQ(date);
        }

        public object GetDoanhThuNhanVienTrongKhoangThoiGian_LinQ(DateTime dateBegin, DateTime dateEnd)
        {
            return _rep.GetDoanhThuNhanVienTrongKhoangThoiGian_LinQ(dateBegin, dateEnd);
        }
    }
}
