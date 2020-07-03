using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.BLL;
    using LTCSDL.Common.Req;

    public class SuppliersSvc : GenericSvc<SuppliersRep, Suppliers>
    {
        public object ThemMoiSupplier(SuppliersReq req)
        {
            return _rep.ThemMoiSupplier(req);
        }

        public object CapNhatSupplier(SuppliersReq req)
        {
            return _rep.CapNhatSupplier(req);
        }
    }
}
