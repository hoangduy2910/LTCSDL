using System;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.BLL;

    public class OrdersSvc : GenericSvc<OrdersRep, Orders>
    {
        public object GetDSDonHangTrongKhoangThoiGian(int size, int page, DateTime dateF, DateTime dateT)
        {
            return _rep.GetDSDonHangTrongKhoangThoiGian(size, page, dateF, dateT);
        }

        public object GetChiTietDonHang(int MaDH)
        {
            return _rep.GetChiTietDonHang(MaDH);
        }

        public object GetDSDonHangTrongKhoangThoiGian_LinQ(int size, int page, DateTime dateF, DateTime dateT)
        {
            return _rep.GetDSDonHangTrongKhoangThoiGian_LinQ(size, page, dateF, dateT);
        }

        public object GetChiTietDonHang_LinQ(int MaDH)
        {
            return _rep.GetChiTietDonHang_LinQ(MaDH);
        }

        public object GetDSDonHangNhanVienTrongKhoangThoiGianTheoKeyword(int size, int page, string keyword, DateTime dateF, DateTime dateT)
        {
            return _rep.GetDSDonHangNhanVienTrongKhoangThoiGianTheoKeyword(size, page, keyword, dateF, dateT);
        }

        public object GetDoanhThuTheoQuocGia(int month, int year)
        {
            return _rep.GetDoanhThuTheoQuocGia(month, year);
        }

        public object GetDoanhThuTheoQuocGia_LinQ(int month, int year)
        {
            return _rep.GetDoanhThuTheoQuocGia_LinQ(month, year);
        }

        public object TimKiemOrderTheoCompanyNameVaEmployeeName(int size, int page, string companyName, string employeeName)
        {
            return _rep.TimKiemOrderTheoCompanyNameVaEmployeeName(size, page, companyName, employeeName);
        }

        public object DanhSachDonHangTrongNgay_LinQ(int size, int page, DateTime date)
        {
            return _rep.DanhSachDonHangTrongNgay_LinQ(size, page, date);
        }

        public object SoLuongHangCanGiaoTrongKhoangThoiGian_LinQ(DateTime dateFrom, DateTime dateTo)
        {
            return _rep.SoLuongHangCanGiaoTrongKhoangThoiGian_LinQ(dateFrom, dateTo);
        }
    }
}
