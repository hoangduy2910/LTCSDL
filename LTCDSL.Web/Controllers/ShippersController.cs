using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    using BLL;
    using Common.Rsp;
    using LTCSDL.Common.Req;

    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        public ShippersController()
        {
            _svc = new ShippersSvc();
        }

        private readonly ShippersSvc _svc;

        [HttpPost("insert-new-shipper")]
        public IActionResult ThemMoiShipper([FromBody] ShippersReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.ThemMoiShipper(req);
            return Ok(res);
        }
    }
}