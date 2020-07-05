using System;
using System.Linq;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.BLL;
    using LTCSDL.Common.Req;

    public class ProductsSvc : GenericSvc<ProductsRep, Products>
    {
        public object GetDSMatHangBanChayNhat(int size, int page, int month, int year)
        {
            return _rep.GetDSMatHangBanChayNhat(size, page, month, year);
        }

        public object GetDSMatHangBanChayNhat_LinQ(int size, int page, int month, int year)
        {
            return _rep.GetDSMatHangBanChayNhat_LinQ(size, page, month, year);
        }

        public object GetDanhSachProductKhongCoDonHangTrongNgay(int size, int page, DateTime date)
        {
            return _rep.GetDanhSachProductKhongCoDonHangTrongNgay(size, page, date);
        }

        public object ThemMoiProduct(InsertProductReq req)
        {
            return _rep.ThemMoiProduct(req);
        }
    }
}
