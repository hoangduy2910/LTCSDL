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

        public object CapNhatShipper(ShippersReq req)
        {
            return _rep.CapNhatShipper(req);
        }

        public object DoanhThuShipperTrongKhoangThoiGian(DoanhThuShipperReq req)
        {
            return _rep.DoanhThuShipperTrongKhoangThoiGian(req.dateBegin, req.dateEnd);
        }
    }
}
