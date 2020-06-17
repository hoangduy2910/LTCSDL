using System;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.BLL;

    public class OrdersSvc : GenericSvc<OrdersRep, Orders>
    {
        public object GetDSDonHangNhanVienTrongKhoangThoiGianTheoKeyword(int size, int page, string keyword, DateTime dateF, DateTime dateT)
        {
            return _rep.GetDSDonHangNhanVienTrongKhoangThoiGianTheoKeyword(size, page, keyword, dateF, dateT);
        }
    }
}
