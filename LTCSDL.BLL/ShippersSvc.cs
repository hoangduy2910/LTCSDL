using System;
using System.Linq;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.BLL;
    using LTCSDL.Common.Req;

    public class ShippersSvc : GenericSvc<ShippersRep, Shippers>
    {
        public Shippers ThemMoiShipper(ShippersReq req)
        {
            Shippers ship = new Shippers();
            ship.CompanyName = req.CompanyName;
            ship.Phone = req.Phone;
            return _rep.ThemMoiShipper(ship);
        }
    }
}
