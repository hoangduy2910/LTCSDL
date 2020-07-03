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
    public class SuppliersController : ControllerBase
    {
        public SuppliersController()
        {
            _svc = new SuppliersSvc();
        }

        private readonly SuppliersSvc _svc;

        [HttpPost("them-moi-supplier")]
        public IActionResult ThemMoiSupplier([FromBody] SuppliersReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.ThemMoiSupplier(req);
            return Ok(res);
        }

        [HttpPost("cap-nhat-supplier")]
        public IActionResult CapNhatSupplier([FromBody] SuppliersReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.CapNhatSupplier(req);
            return Ok(res);
        }
    }
}