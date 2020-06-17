using System;
using System.Linq;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.BLL;

    public class ProductsSvc : GenericSvc<ProductsRep, Products>
    {
        public object GetDSMatHangBanChayNhat(int size, int page, int month, int year)
        {
            return _rep.GetDSMatHangBanChayNhat(size, page, month, year);
        }
    }
}
